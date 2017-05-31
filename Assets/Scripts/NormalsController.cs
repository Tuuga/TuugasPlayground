using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalsController : MonoBehaviour {

	public float movSpeed;
	public float rotSpeed;

	void Update () {
		transform.up = Vector3.Lerp(transform.up, GetOrientation().normalized, 0.1f);
		transform.position += GetMovement();
	}

	Vector3 GetMovement () {
		var movement = GetDir();
		if (movement.magnitude > 1) { movement = movement.normalized; }
		return movement * Time.deltaTime * movSpeed;
	}

	Vector3 GetDir () {
		var fwd = transform.forward * Input.GetAxis("Vertical");
		var right = transform.right * Input.GetAxis("Horizontal");
		return fwd + right;
	}
	
	Vector3 GetOrientation () {
		Ray belowRay = new Ray(transform.position + (transform.up * 0.5f), -transform.up);
		RaycastHit belowHit;

		Ray movRay = new Ray(transform.position + (transform.up * 0.5f), -transform.up + GetDir());
		RaycastHit movHit;

		var orientation = Vector3.zero;
		
		if (Physics.Raycast(belowRay, out belowHit)) {
			transform.position = belowHit.point;
		}
		if (Physics.Raycast(movRay, out movHit)) {
			Debug.DrawRay(movRay.origin, movRay.direction, Color.green, 0, false);
			Debug.DrawRay(movHit.point, movHit.normal, Color.red, 0, false);
		}

		orientation = Vector3.Lerp(belowHit.normal, movHit.normal, 0.5f);

		print(orientation);
		
		return orientation;
	}
}
