using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MergeSortScript : MonoBehaviour {

	public Text unsorted;
	public Text sorted;

	public int listSize;
	public int maxValue;

	public List<int> randomList;

	void Start () {
		randomList = new List<int>();
		for (int i = 0; i < listSize; i++) {
			int randomInt = Random.Range(0, maxValue);
			randomList.Add(randomInt);
		}
		unsorted.text = ListToString(randomList);
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.R)) {
			//sorted.text = ListToString(MergeSort(randomList));
			MergeSort(randomList);
		}

		if (Input.GetKeyDown(KeyCode.W)) {
			List<int> l1 = new List<int>();
			List<int> l2 = new List<int>();

			// Divides the list a into 2 lists
			for (int i = 0; i < randomList.Count; i++) {
				if (i <= randomList.Count / 2)
					l1.Add(randomList[i]);
				else
					l2.Add(randomList[i]);
			}

			for (int i = 0; i < l1.Count; i++) {
				print("" + l1[i]);
			}
			for (int i = 0; i < l2.Count; i++) {
				print("" + l2[i]);
			}
		}
	}

	List<int> MergeSort (List<int> a) {
		//print(a);
		if (a.Count <= 1)
			return a;

		List<int> l1 = new List<int>();
		List<int> l2 = new List<int>();

		// Divides the list a into 2 lists
		for (int i = 0; i < a.Count; i++) {
			if (i <= a.Count / 2)
				l1.Add(a[i]);
			else
				l2.Add(a[i]);
		}

		//print(l1 + "" + l2);

		l1 = MergeSort(l1);
		l2 = MergeSort(l2);

		return Merge(l1, l2);
	}

	List<int> Merge (List<int> a, List<int> b) {
		List<int> c = new List<int>();

		while (a.Count > 0 && b.Count > 0) {
			//print("While 1");
			if (a[0] > b[0]) {
				c.Add(a[0]);
				a.Remove(a[0]);
			} else {
				c.Add(b[0]);
				b.Remove(b[0]);
			}
		}

		while (a.Count > 0) {
			//print("While 2");
			c.Add(a[0]);
			a.Remove(a[0]);
		}

		while (b.Count > 0) {
			//print("While 3");
			c.Add(b[0]);
			b.Remove(b[0]);
		}

		return c;
	}

	string ListToString (List<int> l) {
		string s = "";
		for (int i = 0; i < l.Count; i++) {
			s += l[i] + " ";
		}
		return s;
	}
}
