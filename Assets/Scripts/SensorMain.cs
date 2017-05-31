using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorMain : MonoBehaviour {
	public enum SensorLocation { Entrance, LivingRoom, Kitchen }
	public SensorLocation location;

	bool register;
	Hub hub;

	void Start () {
		hub = FindObjectOfType<Hub>();
	}

	public void Trigger (Sensor.SensorType t) {
		register = !register;
		if (register) {
			hub.SendToHub(this, t);
		}
	}
}
