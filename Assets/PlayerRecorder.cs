using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour {

	struct PlayerState {
		public float time;
		public Vector3 position;
		public Quaternion rotation;

		public PlayerState (float time, Vector3 position, Quaternion rotation) {
			this.time = time;
			this.position = position;
			this.rotation = rotation;
		}
	}

	public float recordLength;
	public bool record;

	List<PlayerState> playerRecord = new List<PlayerState>();

	void Update () {
		if (record) {
			playerRecord.Add(new PlayerState(Time.time, transform.position, transform.rotation));
			var oldestRecord = playerRecord[0];
			print(oldestRecord.time + recordLength - Time.time);
			if (Time.time > oldestRecord.time + recordLength) {
				playerRecord.Remove(oldestRecord);
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			StartCoroutine(Rewind());
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			ToggleRecord();
		}
	}

	IEnumerator Rewind () {
		record = false;
		while (playerRecord.Count > 0) {
			var nextRecord = playerRecord[playerRecord.Count - 1];
			transform.position = nextRecord.position;
			transform.rotation = nextRecord.rotation;
			playerRecord.Remove(nextRecord);
			yield return new WaitForEndOfFrame();
		}
	}

	void ToggleRecord () {
		record = !record;
	}
}
