using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.IO;

public class ShowCode : MonoBehaviour {

	public TextAsset fileToShow;

	Text text;

	void Start () {
		text = GetComponent<Text>();

		string fileText = File.ReadAllText(AssetDatabase.GetAssetPath(fileToShow));

		if (fileText.Contains("public")) {
			var firstIndex = fileText.IndexOf("public");
			var lastIndex = fileText.LastIndexOf("public");

			print(firstIndex + " - " + lastIndex);
		}

		text.text = fileText;
	}
}
