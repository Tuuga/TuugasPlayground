using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {
	public enum SensorType { Enter, Exit}
	public SensorType type;

	SensorMain main;
	void Start () {
		main = GetComponentInParent<SensorMain>();
	}

	void OnTriggerEnter (Collider c) {
		main.Trigger(type);
	}
}
