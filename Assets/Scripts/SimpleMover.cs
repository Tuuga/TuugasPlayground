using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour {

	public float speed;
	public float rotationSpeed;

	void Update () {
		var dir = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
		if (dir.magnitude > 1) { dir = dir.normalized; }
		transform.position += dir * speed * Time.deltaTime;

		if (Input.GetKey(KeyCode.O)) {
			transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
		}
		if (Input.GetKey(KeyCode.P)) {
			transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
		}
	}
}
