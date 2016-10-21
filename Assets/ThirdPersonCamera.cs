using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	public GameObject mainCam;
	public Transform idealCameraPos;
	public Transform pivot;

	public float mouseSens;
	public float upDownRange;

	public float offWall;

	float verticalRotation;
	//float horizontalRotation;

	void Update () {

		// Pivot rotation
		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		//horizontalRotation += Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
		pivot.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0) * Quaternion.identity;

		// Camera Z movement
		RaycastHit hit;
		var dir = idealCameraPos.position - transform.position;
		Debug.DrawRay(transform.position, dir);

		if (Physics.Raycast(transform.position, dir, out hit, dir.magnitude)) {
			var camPos = mainCam.transform.position;
			mainCam.transform.position = hit.point - (dir.normalized * offWall);
		} else {
			mainCam.transform.position = idealCameraPos.position;
		}
	}
}
