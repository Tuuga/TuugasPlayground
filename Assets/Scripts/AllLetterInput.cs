using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AllLetterInput : MonoBehaviour {

	public Text scoreText;
	public Text mistakeText;
	public Text toWriteText;

	public string stringToWrite;
	public string keyboardString = "qwertyuiopasdfghjklzxcvbnm,.-";
	public List<Transform> nodes;
	public List<Color> keysToHitColors;
	public Color lastHitKeyColor;
	Color normalKeyColor;
	public List<int> keysToHit;

	int score;
	int mistakes;
	int lastKeyHit;

	void Start () {

		normalKeyColor = nodes[0].Find("Key Background").GetComponent<Image>().color;

		keysToHit = new List<int>();
		for (int i = 0; i < stringToWrite.Length; i++) {
			for (int j = 0; j < keyboardString.Length; j++) {
				if (stringToWrite[i] == keyboardString[j]) {
					keysToHit.Add(j);
					j = keyboardString.Length;
				}
			}
		}

		
		for (int i = 0; i < keysToHitColors.Count; i++) {
			keysToHit.Add(i);
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

		for (int i = 0; i < nodes.Count; i++) {
			nodes[i].Find("Key Background").GetComponent<Image>().color = normalKeyColor;
			if (i == lastKeyHit) {
				nodes[i].Find("Key Background").GetComponent<Image>().color = lastHitKeyColor;
			}
		}

		for (int i = 0; i < keysToHitColors.Count; i++) {
			nodes[keysToHit[i]].Find("Key Background").GetComponent<Image>().color = keysToHitColors[i];
		}
	}

	void UpdateKeysToHit () {

		keysToHit.RemoveAt(0);
		stringToWrite = stringToWrite.Remove(0,1);
		for (int i = 0; i < keysToHitColors.Count - 1; i++) {
			keysToHit[i] = keysToHit[i + 1];
		}

		/*
		int nextRandom = Random.Range(0, nodes.Count);
		while (keysToHit.Contains(nextRandom) || nextRandom == lastKeyHit) {
			nextRandom = Random.Range(0, nodes.Count);
		}
		keysToHit[keysToHit.Count - 1] = nextRandom;
		*/
	}

	void AllInputs () {
		// Condensed input code
		for (int i = 0; i < keyboardString.Length; i++)
			if (Input.GetKeyDown(keyboardString[i].ToString()))
				KeyHit(i);

		if (Input.GetKeyDown(KeyCode.Space)) {
			KeyHit(29);
		}
	}
}
