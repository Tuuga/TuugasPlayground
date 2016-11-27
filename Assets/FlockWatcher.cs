using UnityEngine;
using System.Collections;

public class FlockWatcher : MonoBehaviour {

	public GameObject ind;
	FlockAgent[] flock;
	Vector3 center;

	void Start () {
		flock = FindObjectsOfType<FlockAgent>();
	}
	
	void Update () {
		var v = new Vector3();
		foreach (FlockAgent fa in flock) {
			v += fa.transform.position;
		}

		center = v / flock.Length;
		ind.transform.position = center;
		transform.LookAt(center);
	}
}
