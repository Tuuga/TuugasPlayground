using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AllLetterInput : MonoBehaviour {

	public Text scoreText;
	public Text mistakeText;

	//public List<Transform> enemies;
	//public GameObject player;
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
		//UpdateKeyColors();
	}

	void Update () {
		AllInputs();
	}

	void UpdateText () {
		scoreText.text = "Score: " + score;
		mistakeText.text = "Mistakes: " + mistakes;
	}
	void KeyHit (int i) {
		//player.transform.position = nodes[i].position;

		lastKeyHit = i;

		if (lastKeyHit == keysToHit[0]) {
			//MoveEnemy();
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
			nodes[keysToHit[i]].Find("Key Background").GetComponent<Image>().color = keysToHitColors[i];
		}

		int nextRandom = Random.Range(0, nodes.Count);
		while (keysToHit.Contains(nextRandom) || nextRandom == lastKeyHit) {
			nextRandom = Random.Range(0, nodes.Count);
		}

		keysToHit[keysToHit.Count - 1] = nextRandom;
		nodes[keysToHit[keysToHit.Count - 1]].Find("Key Background").GetComponent<Image>().color = keysToHitColors[keysToHit.Count - 1];

		/*
		for (int i = keysToHit.Count - 1; i > 0; i--) {
			print(i);
			keysToHit[i] = keysToHit[i - 1];
			nodes[keysToHit[i]].Find("Key Background").GetComponent<Image>().color = keysToHitColors[i];
		}
		*/
	}

	/*
	void MoveEnemy () {

		for (int i = keysToHit.Count - 1; i > 0; i--) {
			keysToHit[i] = keysToHit[i - 1];
			enemies[i].position = nodes[keysToHit[i]].position;
		}

		int lastIndex = keysToHit[0];
		keysToHit[0] = Random.Range(0, nodes.Count);
		// "<" button not working
		while (keysToHit[0] == lastIndex || CheckAllIndexes(keysToHit[0])) {
			keysToHit[0] = Random.Range(0, nodes.Count);
			enemies[0].position = nodes[keysToHit[0]].position;
		}
	}

	bool CheckAllIndexes (int value) {
		for (int i = 1; i < keysToHit.Count; i++) {
			if (value == keysToHit[i]) {
				return true;
			}
		}
		return false;
	}
	*/
	void AllInputs () {

		if (Input.GetKeyDown(KeyCode.Q)) {
			KeyHit(0);
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			KeyHit(1);
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			KeyHit(2);
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			KeyHit(3);
		}
		if (Input.GetKeyDown(KeyCode.T)) {
			KeyHit(4);
		}
		if (Input.GetKeyDown(KeyCode.Y)) {
			KeyHit(5);
		}
		if (Input.GetKeyDown(KeyCode.U)) {
			KeyHit(6);
		}
		if (Input.GetKeyDown(KeyCode.I)) {
			KeyHit(7);
		}
		if (Input.GetKeyDown(KeyCode.O)) {
			KeyHit(8);
		}
		if (Input.GetKeyDown(KeyCode.P)) {
			KeyHit(9);
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			KeyHit(10);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			KeyHit(11);
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			KeyHit(12);
		}
		if (Input.GetKeyDown(KeyCode.F)) {
			KeyHit(13);
		}
		if (Input.GetKeyDown(KeyCode.G)) {
			KeyHit(14);
		}
		if (Input.GetKeyDown(KeyCode.H)) {
			KeyHit(15);
		}
		if (Input.GetKeyDown(KeyCode.J)) {
			KeyHit(16);
		}
		if (Input.GetKeyDown(KeyCode.K)) {
			KeyHit(17);
		}
		if (Input.GetKeyDown(KeyCode.L)) {
			KeyHit(18);
		}
		if (Input.GetKeyDown(KeyCode.Z)) {
			KeyHit(19);
		}
		if (Input.GetKeyDown(KeyCode.X)) {
			KeyHit(20);
		}
		if (Input.GetKeyDown(KeyCode.C)) {
			KeyHit(21);
		}
		if (Input.GetKeyDown(KeyCode.V)) {
			KeyHit(22);
		}
		if (Input.GetKeyDown(KeyCode.B)) {
			KeyHit(23);
		}
		if (Input.GetKeyDown(KeyCode.N)) {
			KeyHit(24);
		}
		if (Input.GetKeyDown(KeyCode.M)) {
			KeyHit(25);
		}
		if (Input.GetKeyDown(KeyCode.Comma)) {
			KeyHit(26);
		}
		if (Input.GetKeyDown(KeyCode.Period)) {
			KeyHit(27);
		}
		if (Input.GetKeyDown(KeyCode.Minus)) {
			KeyHit(28);
		}
	}
}
