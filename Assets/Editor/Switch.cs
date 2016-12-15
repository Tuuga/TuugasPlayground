using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Switch : EditorWindow {

	Camera[] cameras;
	bool showRefresh = true, clampDown;

	[MenuItem("Window/Switch")]
	static void Init() {
		Switch window = (Switch)EditorWindow.GetWindow(typeof(Switch));
		window.Show();
	}

	void OnGUI() {
		GUILayout.BeginHorizontal();
		GUILayout.Label("All Cameras have to be active to refresh");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		showRefresh = GUILayout.Toggle(showRefresh, "Show refresh");
		if (showRefresh && GUILayout.Button("Refresh list")) {
			var foundCameras = FindObjectsOfType<Camera>();
			if (foundCameras.Length > 1) {
				cameras = foundCameras;
			}
		}
		GUILayout.EndHorizontal();

		clampDown = GUILayout.Toggle(clampDown, "Clamp down");
		if (clampDown) { GUILayout.FlexibleSpace(); }

		if (cameras == null || cameras.Length == 0) { return; }

		for (int i = 0; i < cameras.Length; i++) {
			if (GUILayout.Button(cameras[i].name)) {
				cameras[i].gameObject.SetActive(true);
				for (int j = 0; j < cameras.Length; j++) {
					if (cameras[i] != cameras[j]) {
						cameras[j].gameObject.SetActive(false);
					}
				}
				break;
			}
		}
	}
}
