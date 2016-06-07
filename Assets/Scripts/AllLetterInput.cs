using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public struct Key {
	public Image visual;
	public string keyString;
	public int index;

	public Key (string s, Image im, int i) {
		visual = im;
		keyString = s;
		index = i;
	}
	public bool KeyInput () {
		if (Input.GetKeyDown(keyString))
			return true;
		return false;
	}
};

public class AllLetterInput : MonoBehaviour {

	public Text scoreText;
	public Text mistakeText;
	public Text toWriteText;

	public string stringToWrite;
	public string keyboardString = "qwertyuiopasdfghjklzxcvbnm,.-";
	public List<Color> keysToHitColors;
	public Color lastHitKeyColor;
	Color normalKeyColor;
	List<int> keysToHit = new List<int>();

	int score;
	int mistakes;
	int lastKeyHit;

	List<Key> keys = new List<Key>();
	// For edge cases like "space"
	Dictionary<string, Key> keysDict = new Dictionary<string, Key>();

	void InitialKeys () {
		for (int i = 0; i < keyboardString.Length; i++) {
			Image newVisual = GameObject.Find(keyboardString[i].ToString().ToUpper()).transform.Find("Key Background").GetComponent<Image>();
			Key newKey = new Key(keyboardString[i].ToString(), newVisual, i);
			keys.Add(newKey);
		}
		Image spaceVisual = GameObject.Find("space").transform.Find("Key Background").GetComponent<Image>();
		keys.Add(new Key("space", spaceVisual, keys.Count));

		for (int i = 0; i < keys.Count; i++) {
			keysDict.Add(keys[i].keyString, keys[i]);
		}
	}

	void Start () {

		InitialKeys();

		normalKeyColor = keys[0].visual.color;

		keysToHit = new List<int>();
		for (int i = 0; i < stringToWrite.Length; i++) {
			for (int j = 0; j < keys.Count; j++) {
				if (stringToWrite[i] == keyboardString[j]) {
					keysToHit.Add(j);
					j = keys.Count;
				} else if (stringToWrite[i].ToString() == " ") {
					keysToHit.Add(keysDict["space"].index);
					j = keys.Count;
				}
			}
		}

		UpdateKeyColors();
		UpdateText();
	}

	void Update () {
		AllInputs();
	}

	void UpdateText () {
		scoreText.text = "Score: " + score;
		mistakeText.text = "Mistakes: " + mistakes;
		toWriteText.text = stringToWrite.ToUpper();
	}
	void KeyHit (int i) {
		lastKeyHit = i;

		if (lastKeyHit == keysToHit[0]) {
			UpdateKeysToHit();
			score++;
		} else {
			mistakes++;
		}
		UpdateText();
		UpdateKeyColors();
	}

	void UpdateKeyColors () {

		for (int i = 0; i < keys.Count; i++) {
			keys[i].visual.color = normalKeyColor;
			if (i == lastKeyHit) {
				keys[i].visual.color = lastHitKeyColor;
			}
		}

		for (int i = 0; i < keysToHitColors.Count; i++) {
			keys[keysToHit[i]].visual.color = keysToHitColors[i];
		}
	}

	void UpdateKeysToHit () {

		keysToHit.RemoveAt(0);
		stringToWrite = stringToWrite.Remove(0,1);
		for (int i = 0; i < keysToHitColors.Count - 1; i++) {
			keysToHit[i] = keysToHit[i + 1];
		}
	}

	void AllInputs () {
		// Condensed input code
		for (int i = 0; i < keys.Count; i++)
			if (keys[i].KeyInput())
				KeyHit(i);
	}
}
