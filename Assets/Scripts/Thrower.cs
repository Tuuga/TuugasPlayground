using UnityEngine;
using System.Collections;

public class Thrower : MonoBehaviour {

	public GameObject obj;
	public float force;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			var objIns = (GameObject)Instantiate(obj, transform.position, transform.rotation);
			var rb = objIns.GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * force, ForceMode.Impulse);
		}
	}
}
