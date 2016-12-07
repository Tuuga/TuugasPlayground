using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.IO;

public class ShowCode : MonoBehaviour {
	
	public TextAsset fileToShow;

	public string[] textToCheck;
	public string[] colorCode;

	Text text;

	void Start () {
		text = GetComponent<Text>();
		var fileText = File.ReadAllText(AssetDatabase.GetAssetPath(fileToShow));

		fileText = SetColors(fileText);
		text.text = fileText;
	}

	string SetColors(string s) {
		// /color changed to \color to not break the coloring
		s = s.Replace("</color>", "<\\color>");

		for (int i = 0; i < textToCheck.Length; i++) {
			bool found = s.Contains(textToCheck[i]);
			int firstIndex = 0;
			int count = 0;
			int whileRunCount = 0;
			while (firstIndex > -1) {
				whileRunCount++;

				firstIndex = s.IndexOf(textToCheck[i], firstIndex);
				int lastIndex = firstIndex + textToCheck[i].Length;

				if (firstIndex > -1) {
					count++;
					s = s.Insert(lastIndex, "</color>");
					s = s.Insert(firstIndex, colorCode[i]);

					firstIndex += ("</color>" + colorCode[i]).Length;
				}

				if (whileRunCount > s.Length) {
					Debug.LogError("While ran longer than fileText lenght");
					break;
				}
			}
		}
		return s;
	}
}



