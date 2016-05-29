using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AllLetterInput : MonoBehaviour {

	public Text scoreText;
	public Text mistakeText;

	public string keyboardString = "qwertyuiopasdfghjklzxcvbnm,.-";
	public List<Transform> nodes;
	public List<Color> keysToHitColors;
	public Color lastHitKeyColor;
	Color normalKeyColor;
	List<int> keysToHit;

	int score;
	int mistakes;
	int lastKeyHit;

	void Start () {

		normalKeyColor = nodes[0].Find("Key Background").GetComponent<Image>().color;

		keysToHit = new List<int>();
		for (int i = 0; i < keysToHitColors.Count; i++) {
			keysToHit.Add(i);
		}
		UpdateKeysToHit();
		UpdateKeyColors();
	}

	void Update () {
		AllInputs();
	}

	void UpdateText () {
		scoreText.text = "Score: " + score;
		mistakeText.text = "Mistakes: " + mistakes;
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

		for (int i = 0; i < keysToHit.Count; i++) {
			nodes[keysToHit[i]].Find("Key Background").GetComponent<Image>().color = keysToHitColors[i];
		}

		for (int i = 0; i < nodes.Count; i++) {
			if (i == lastKeyHit) {
				nodes[i].Find("Key Background").GetComponent<Image>().color = lastHitKeyColor;
			} else if (!keysToHit.Contains(i)) {
				nodes[i].Find("Key Background").GetComponent<Image>().color = normalKeyColor;
			}
		}
	}

	void UpdateKeysToHit () {

		for (int i = 0; i < keysToHit.Count - 1; i++) {
			keysToHit[i] = keysToHit[i + 1];
		}

		int nextRandom = Random.Range(0, nodes.Count);
		while (keysToHit.Contains(nextRandom) || nextRandom == lastKeyHit) {
			nextRandom = Random.Range(0, nodes.Count);
		}
		keysToHit[keysToHit.Count - 1] = nextRandom;
	}

	void AllInputs () {
		// Condensed input code
		for (int i = 0; i < nodes.Count; i++)
			if (Input.GetKeyDown(keyboardString[i].ToString()))
				KeyHit(i);
	}
}
