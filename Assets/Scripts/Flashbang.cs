using UnityEngine;
using System.Collections;

public class Flashbang : MonoBehaviour {

	void Start () {
		StartCoroutine(Bang(1f));
	}

	IEnumerator Bang (float time) {
		yield return new WaitForSeconds(time);

		var mainCamPos = Camera.main.transform.position;
		var fromThisToCam = mainCamPos - transform.position;
		var dir = fromThisToCam.normalized;
		var dist = fromThisToCam.magnitude;
		RaycastHit hit;

		if (Physics.Raycast(transform.position, dir, out hit)) {
			print (Vector3.Angle(dir, Camera.main.transform.forward));
		}
	}
}
