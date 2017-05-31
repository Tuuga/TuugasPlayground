using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour {

	public int inHouse;
	public int inKitchen;
	public int inLivingRoom;

	public Light entrance, kitchen, livingRoom;

	public void SendToHub (SensorMain s, Sensor.SensorType t) {
		//print(t.ToString() + " " + s.location.ToString());

		if (s.location == SensorMain.SensorLocation.Entrance) {
			if (t == Sensor.SensorType.Enter) {
				inHouse++;
			} else {
				inHouse--;
			}
		} else if (s.location == SensorMain.SensorLocation.LivingRoom) {
			if (t == Sensor.SensorType.Enter) {
				inLivingRoom++;
				inKitchen--;
			} else {
				inLivingRoom--;
				inKitchen++;
			}
		} else if (s.location == SensorMain.SensorLocation.Kitchen) {
			if (t == Sensor.SensorType.Enter) {
				inKitchen++;
			} else {
				inKitchen--;
			}
		}

		if (inKitchen > 0) {
			kitchen.enabled = true;
		} else {
			kitchen.enabled = false;
		}

		if (inLivingRoom > 0) {
			livingRoom.enabled = true;
		} else {
			livingRoom.enabled = false;
		}

		if (inHouse == 0 || inLivingRoom + inKitchen == inHouse) {
			entrance.enabled = false;
		} else {
			entrance.enabled = true;
		}
	}
}
