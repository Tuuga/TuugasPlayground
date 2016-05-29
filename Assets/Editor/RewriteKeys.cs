using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[ExecuteInEditMode]
public class RewriteKeys : EditorWindow {

	[MenuItem("Window/RewriteKeys")]
	static void Init () {
		RewriteKeys window = (RewriteKeys)EditorWindow.GetWindow(typeof(RewriteKeys));
		window.Show();
	}

	void OnGUI () {
		if (GUILayout.Button("Rewrite Keys")) {
			GameObject[] keys = GameObject.FindGameObjectsWithTag("KeyText");
			for (int i = 0; i < keys.Length; i++) {
				keys[i].GetComponent<Text>().text = keys[i].transform.parent.parent.name;
			}
		}
	}
}
