using UnityEngine;
using System.Collections;

public class Coroutines : MonoBehaviour {

	void Start () {
		StartCoroutine(MyUpdate());
	}

	IEnumerator MyUpdate () {
		while (true) {

			yield return new WaitForEndOfFrame();
		}
	}
}
