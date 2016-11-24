using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlockAgent : MonoBehaviour {

	public float neigborhoodDist;
	public float separationDist;
	public float lerpSpeed;
	public float movementSpeed;

	[Range(0, 1f)]
	public float alignmentWeight, cohesionWeight, separationWeight, avoidWeight = 1f;
	
	[SerializeField]
	public Vector3 vector;

	void Start () {
		vector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
	}
	
	void Update () {
		var newVec = Flocking();
		newVec.Normalize();
		vector = Vector3.Lerp(vector, newVec, lerpSpeed);
		transform.LookAt(transform.position + vector);
		transform.position += vector * Time.deltaTime * movementSpeed;
	}

	Vector3 Flocking () {
		var agents = FindObjectsOfType<FlockAgent>();
		

		Vector3 alignment = new Vector3();
		Vector3 cohesion = new Vector3();
		Vector3 separation = new Vector3();
		

		var neighborCount = 0;
		var separationCount = 0;

		foreach(FlockAgent fa in agents) {
			if (fa != this) {
				var dist = Vector3.Distance(transform.position, fa.transform.position);
				if (dist < neigborhoodDist) {
					neighborCount++;
					alignment += fa.vector;
					cohesion += fa.transform.position;
					if (dist < separationDist) {
						separationCount++;
						separation += fa.transform.position - transform.position;
					}
				}
			}
		}
		
		Vector3 avoid = new Vector3();
		var obstacles = FindObjectsOfType<FlockObstacle>();
		var obstacleCount = 0;
		var avoidStrength = 0f;
		foreach (FlockObstacle fo in obstacles) {
			var dist = Vector3.Distance(transform.position, fo.transform.position);
			var dir = (fo.transform.position - transform.position).normalized;
			var dot = Vector3.Dot(transform.forward, dir);

			if (dist < fo.radius && dot > 0) {
				obstacleCount++;
				avoid += fo.transform.position - transform.position;
				avoidStrength += dot;
			}
		}

		alignment /= neighborCount;
		cohesion /= neighborCount;
		separation /= separationCount;
		avoid /= obstacleCount;
		avoidStrength /= obstacleCount;

		cohesion = (cohesion - transform.position);
		separation *= -1f;
		avoid *= -avoidStrength;

		alignment.Normalize();
		cohesion.Normalize();
		separation.Normalize();
		avoid.Normalize();
		Vector3 v = alignment * alignmentWeight + cohesion * cohesionWeight + separation * separationWeight + avoid * avoidWeight;

		//Debug.DrawLine(transform.position, transform.position + v, Color.red);

		return v.normalized;
	}
}
