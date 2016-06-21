using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InsertionSortScript : MonoBehaviour {

	public int listSize;
	List<int> randomList;

	void Start () {
		RandomizeList();
		PrintList(randomList, "<color=red>");
		InsertionSort(randomList);
		PrintList(randomList, "<color=green>");
	}

	public List<int> InsertionSort (List<int> a) {
		int pos;
		int value;

		for (int i = 1; i < a.Count; i++) {
			value = a[i];
			pos = i;

			while (pos > 0 && a[pos-1] > value) {
				a[pos] = a[pos - 1];
				pos--;
			}
			a[pos] = value;
		}
		return a;
	}

	void RandomizeList() {
		randomList = new List<int>();
		for (int i = 0; i < listSize; i++) {
			int randomInt = Random.Range(0, listSize);
			randomList.Add(randomInt);
		}
	}

	void PrintList (List<int> l) {
		for (int i = 0; i < l.Count; i++) {
			print(l[i]);
		}
	}

	void PrintList(List<int> l, string tag) {
		for (int i = 0; i < l.Count; i++) {
			print(tag + l[i] + "</color>");
		}
	}
}
