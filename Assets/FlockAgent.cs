using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlockAgent : MonoBehaviour {

	public float comeBackDist = 100f;

	public float neigborhoodDist;
	public float separationDist;
	public float lerpSpeed;
	public float movementSpeed;

	[Range(0, 1f)]
	public float alignmentWeight, cohesionWeight, separationWeight, avoidWeight, backToZeroWeight = 1f;
	
	public Vector3 vector;
	Vector3 newVec;

	List<FlockAgent> agents = new List<FlockAgent>();
	MeshRenderer mr;

	void Start () {
		vector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
		mr = GetComponentInChildren<MeshRenderer>();

		var startAgents = FindObjectsOfType<FlockAgent>();
		foreach (FlockAgent fa in startAgents) {
			var dist = Vector3.Distance(transform.position, fa.transform.position);
			if (dist < neigborhoodDist) {
				agents.Add(fa);
			}
		}
	}
	
	void Update () {
		newVec = Flocking();
		newVec.Normalize();
		vector = Vector3.Lerp(vector, newVec, lerpSpeed);

		var c = new Color(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
		mr.material.color = c;

		transform.position += vector * Time.deltaTime * movementSpeed;
		transform.LookAt(transform.position + vector);
	}

	Vector3 Flocking () {

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

		var inv = new Vector3();
		if (transform.position.magnitude > comeBackDist) {
			inv = -transform.position.normalized;
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
		Vector3 v = alignment * alignmentWeight + cohesion * cohesionWeight + separation * separationWeight + avoid * avoidWeight + inv * backToZeroWeight;

		return v.normalized;
	}

	void OnTriggerEnter (Collider c) {
		var fa = GetComponent<FlockAgent>();
		if (fa) {
			agents.Add(fa);
		}
	}

	void OnTriggerExit (Collider c) {
		var fa = GetComponent<FlockAgent>();
		if (fa) {
			agents.Remove(fa);
		}
	}
}
