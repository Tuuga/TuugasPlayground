using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeBehaviour : MonoBehaviour {

	List<Transform> phaseOneWaypoints;
	//public List<Transform> nodes;

	//public Transform firstNode;

	public float speed;

	public bool randomizeNextNode;

	int _nextNode;

	void Start () {

		GameObject[] foundW = GameObject.FindGameObjectsWithTag("Waypoint");
		phaseOneWaypoints = new List<Transform>(foundW.Length);
		for (int i = 0; i < foundW.Length; i++) {
			phaseOneWaypoints.Add(foundW[i].transform);
		}

		speed = Random.Range(5, 25);
		if (randomizeNextNode) {
			_nextNode = Random.Range(0, phaseOneWaypoints.Count);
		}		
	}

	void Update () {
		//Movement();
		//Follow();
		MoveThis();
	}

	void MoveThis () {
		Vector3 dir = (phaseOneWaypoints[_nextNode].position - transform.position).normalized;
		transform.position += dir * speed * Time.deltaTime;

		if (Vector3.Distance(transform.position, phaseOneWaypoints[_nextNode].position) < 0.2f) {

			if (randomizeNextNode) {
				_nextNode = Random.Range(0, phaseOneWaypoints.Count);
			} else {
				if (_nextNode < phaseOneWaypoints.Count - 1) {
					_nextNode++;
				} else {
					_nextNode = 0;
				}
			}
		}
	}
	/*
	void Movement () {
		//Moves first node
		Vector3 dir = (phaseOneWaypoints[_nextNode].position - firstNode.position).normalized;
		firstNode.position += dir * speed * Time.deltaTime;

		if (Vector3.Distance(firstNode.position, phaseOneWaypoints[_nextNode].position) < 0.2f) {

			if (_nextNode < phaseOneWaypoints.Count - 1) {
				_nextNode++;
			} else {
				_nextNode = 0;
			}
		}
	}
	
	void Follow () {
		//Moves second node
		Vector3 secondNodeDir = (firstNode.position - nodes[0].position).normalized;
		nodes[0].position += secondNodeDir * speed * Time.deltaTime;

		for (int i = 1; i < nodes.Count; i++) { //Moves every other node
			Vector3 dir = (nodes[i - 1].position - nodes[i].position).normalized;
			nodes[i].position += dir * speed * Time.deltaTime;
		}
	}
	*/
}
