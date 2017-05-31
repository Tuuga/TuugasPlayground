using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prime : MonoBehaviour {

	void Start () {
		for (int i = 0; i < 100; i++) {
			print(Mathf.Pow(2, i) - 1);
		}
	}
}
