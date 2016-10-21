using UnityEngine;
using System.Collections;
using System.Threading;

public class ThreadingTest : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			for (int i = 0; i < 100; i++) {
				print(i);
				Thread.Sleep(1000);
			}
		}
	}
}
