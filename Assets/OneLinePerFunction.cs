using UnityEngine;
using System.Collections;

public class OneLinePerFunction : MonoBehaviour {
	
	void Update () {
		transform.position += new Vector3((Input.GetKey("a") ? -1 : 0) + (Input.GetKey("d") ? 1 : 0), 0, (Input.GetKey("w") ? 1 : 0) + (Input.GetKey("s") ? -1 : 0)) * Time.deltaTime * 10f;
	}
}
