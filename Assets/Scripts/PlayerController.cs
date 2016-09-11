using UnityEngine;
using System.Collections;

public class PlayerController: MonoBehaviour {

	public float movSpeed;
	public float sprintSpeed;
	public float crouchSpeed;
	public float walkInterval;
	public float sprintInterval;
	public float mouseSens;
	public float upDownRange;

	float verticalRotation;
	float horizontalRotation;
	float startMovSpeed;
	float stepTimer;

	int stepCount;

	bool mouseLock;

	GameObject mainCam;

	Quaternion baseRotation = Quaternion.identity;
	CapsuleCollider cc;
	void Start () {
		cc = GetComponent<CapsuleCollider>();
		startMovSpeed = movSpeed;
		MouseLock();
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void Update () {

		Movement();

		if (mouseLock)
			MouseLook();

		if (Input.GetKeyDown(KeyCode.LeftAlt))
			MouseLock();
	}

	void MouseLook () {
		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

		horizontalRotation += Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;

		transform.localRotation = Quaternion.Euler(0, horizontalRotation, 0) * baseRotation;
		mainCam.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0) * baseRotation;
	}

	void MouseLock () {
		mouseLock = !mouseLock;
		if (mouseLock) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	void Movement () {
		float playSpeed = walkInterval;
		bool sprinting = false;
		Vector3 moveDir = new Vector3();
		moveDir += transform.forward * Input.GetAxis("Vertical");
		moveDir += transform.right * Input.GetAxis("Horizontal");

		moveDir.y = 0;

		// normalize dir if not smoothing
		if (moveDir.magnitude > 1)
			moveDir.Normalize();

		if (Input.GetButtonDown("Jump")) {
			Jump();
		}

		// Sprint if Shift and running forward or forward diagonally
		if (Input.GetButton("Sprint") && Vector3.Dot(transform.forward, moveDir) > 0) {
			movSpeed = sprintSpeed;
			playSpeed = sprintInterval;
			sprinting = true;
		}

		mainCam.transform.position = transform.position;
		if (Input.GetButton("Crouch")) {
			Crouch();
			playSpeed = walkInterval * (startMovSpeed / crouchSpeed);
		}
		if(Input.GetButtonUp("Crouch")) {
			Stand();
		}

		//Sound
		stepTimer += Time.deltaTime * moveDir.magnitude;
		if(stepTimer >= playSpeed) {
			stepTimer = 0;
			stepCount++;
			if (sprinting) {
				if (stepCount % 2 == 0) {
					//Fabric.EventManager.Instance.PostEvent("RunningStep1");
				} else {
					//Fabric.EventManager.Instance.PostEvent("RunningStep2");
				}
			} else {
				if (stepCount % 2 == 0) {
					//Fabric.EventManager.Instance.PostEvent("WalkingStep1");
				} else {
					//Fabric.EventManager.Instance.PostEvent("WalkingStep2");
				}
			}
		}

		transform.position += moveDir * movSpeed * Time.deltaTime;
		movSpeed = startMovSpeed;
	}

	void Jump () {

	}

	void Crouch () {
		movSpeed = crouchSpeed;
		cc.height = 1;
		cc.center = new Vector3(0, -1f, 0);
		mainCam.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
	}

	void Stand () {
		cc.height = 2;
		cc.center = new Vector3(0, -0.5f, 0);
		mainCam.transform.position = transform.position;
	}
}
