using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MergeSortScript : MonoBehaviour {

	public Text unsorted;
	public Text sorted;
	public Text data;

	public int listSize;

	List<int> randomList;
	List<int> sortedList;

	float highestDeltaTime;

	void Start () {
		RandomizeList();
		unsorted.text = ListToString(randomList);
	}

	void Update () {
		UpdateData();
		UserInputs();
	}

	List<int> MergeSort (List<int> a) {
		if (a.Count == 1) {
			return a;
		}

		List<int> l1 = new List<int>();
		List<int> l2 = new List<int>();

		for (int i = 0; i < a.Count; i++) {
			if (i < a.Count / 2) {
				l1.Add(a[i]);
			} else {
				l2.Add(a[i]);
			}
		}

		l1 = MergeSort(l1);
		l2 = MergeSort(l2);

		return Merge(l1, l2);
	}

	List<int> Merge (List<int> a, List<int> b) {
		List<int> c = new List<int>();

		while (a.Count > 0 && b.Count > 0) {
			if (a[0] < b[0]) {
				c.Add(a[0]);
				a.Remove(a[0]);
			} else {
				c.Add(b[0]);
				b.Remove(b[0]);
			}
		}

		while (a.Count > 0) {
			c.Add(a[0]);
			a.Remove(a[0]);
		}

		while (b.Count > 0) {
			c.Add(b[0]);
			b.Remove(b[0]);
		}

		return c;
	}

	string ListToString (List<int> l) {
		string s = "";
		for (int i = 0; i <= 400; i++) {
			s += l[i] + " ";
		}
		return s;
	}

	void RandomizeList () {
		randomList = new List<int>();
		for (int i = 0; i < listSize; i++) {
			int randomInt = Random.Range(0, listSize);
			randomList.Add(randomInt);
		}
	}

	void UpdateData () {
		if (Time.deltaTime > highestDeltaTime) {
			highestDeltaTime = Time.deltaTime;
		}
		data.text = Mathf.Round(Time.deltaTime * 1000) + " ms - Highest " + Mathf.Round(highestDeltaTime * 1000) + " ms";
	}

	void UserInputs () {
		if (Input.GetKeyDown(KeyCode.R)) {
			RandomizeList();
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			sortedList = MergeSort(randomList);
		}

		if (Input.GetKeyDown(KeyCode.W)) {
			sorted.text = ListToString(sortedList);
			unsorted.text = ListToString(randomList);
		}

		if (Input.GetKeyDown(KeyCode.Q)) {
			RandomizeList();
			sortedList = MergeSort(randomList);
			sorted.text = ListToString(sortedList);
			unsorted.text = ListToString(randomList);
		}
	}
}
