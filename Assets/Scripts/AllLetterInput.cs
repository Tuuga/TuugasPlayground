using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AllLetterInput : MonoBehaviour {

	public Text scoreText;
	public Text mistakeText;

	public List<Transform> nodes;
	public List<Transform> enemies;

	public GameObject player;

	List<int> enemyIndexes;

	int score;
	int mistakes;
	int playerPosIndex;

	void Start () {
		print("asd");
		enemyIndexes = new List<int>();
		for (int i = 0; i < enemies.Count; i++) {
			enemyIndexes.Add(i);
			print(enemyIndexes[i]);
			enemies[i].position = nodes[enemyIndexes[i]].position;
		}
	}

	void Update () {
		AllInputs();
	}

	void UpdateText () {
		scoreText.text = "Score: " + score;
		mistakeText.text = "Mistakes: " + mistakes;
	}

	void MoveToNode (int i) {
		player.transform.position = nodes[i].position;
		playerPosIndex = i;

		if (playerPosIndex == enemyIndexes[0]) {
			MoveEnemy();
			score++;
		} else {
			mistakes++;
		}
		UpdateText();
	}

	void MoveEnemy () {

		for (int i = enemyIndexes.Count - 1; i > 0; i--) {
			enemyIndexes[i] = enemyIndexes[i - 1];
			enemies[i].position = nodes[enemyIndexes[i]].position;
		}

		int lastIndex = enemyIndexes[0];
		enemyIndexes[0] = Random.Range(0, nodes.Count);
		// "<" button not working
		while (enemyIndexes[0] == lastIndex || enemyIndexes[0] == 19 || CheckAllIndexes(enemyIndexes[0])) {
			enemyIndexes[0] = Random.Range(0, nodes.Count);
			enemies[0].position = nodes[enemyIndexes[0]].position;
		}
	}

	bool CheckAllIndexes (int value) {
		for (int i = 1; i < enemyIndexes.Count; i++) {
			if (value == enemyIndexes[i]) {
				return true;
			}
		}
		return false;
	}

	void AllInputs () {

		if (Input.GetKeyDown(KeyCode.Q)) {
			MoveToNode(0);
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			MoveToNode(1);
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			MoveToNode(2);
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			MoveToNode(3);
		}
		if (Input.GetKeyDown(KeyCode.T)) {
			MoveToNode(4);
		}
		if (Input.GetKeyDown(KeyCode.Y)) {
			MoveToNode(5);
		}
		if (Input.GetKeyDown(KeyCode.U)) {
			MoveToNode(6);
		}
		if (Input.GetKeyDown(KeyCode.I)) {
			MoveToNode(7);
		}
		if (Input.GetKeyDown(KeyCode.O)) {
			MoveToNode(8);
		}
		if (Input.GetKeyDown(KeyCode.P)) {
			MoveToNode(9);
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			MoveToNode(10);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			MoveToNode(11);
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			MoveToNode(12);
		}
		if (Input.GetKeyDown(KeyCode.F)) {
			MoveToNode(13);
		}
		if (Input.GetKeyDown(KeyCode.G)) {
			MoveToNode(14);
		}
		if (Input.GetKeyDown(KeyCode.H)) {
			MoveToNode(15);
		}
		if (Input.GetKeyDown(KeyCode.J)) {
			MoveToNode(16);
		}
		if (Input.GetKeyDown(KeyCode.K)) {
			MoveToNode(17);
		}
		if (Input.GetKeyDown(KeyCode.L)) {
			MoveToNode(18);
		}
		if (Input.GetKeyDown(KeyCode.Less)) {
			MoveToNode(19);
		}
		if (Input.GetKeyDown(KeyCode.Z)) {
			MoveToNode(20);
		}
		if (Input.GetKeyDown(KeyCode.X)) {
			MoveToNode(21);
		}
		if (Input.GetKeyDown(KeyCode.C)) {
			MoveToNode(22);
		}
		if (Input.GetKeyDown(KeyCode.V)) {
			MoveToNode(23);
		}
		if (Input.GetKeyDown(KeyCode.B)) {
			MoveToNode(24);
		}
		if (Input.GetKeyDown(KeyCode.N)) {
			MoveToNode(25);
		}
		if (Input.GetKeyDown(KeyCode.M)) {
			MoveToNode(26);
		}
		if (Input.GetKeyDown(KeyCode.Comma)) {
			MoveToNode(27);
		}
		if (Input.GetKeyDown(KeyCode.Period)) {
			MoveToNode(28);
		}
		if (Input.GetKeyDown(KeyCode.Minus)) {
			MoveToNode(29);
		}
	}
}
