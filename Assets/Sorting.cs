using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sorting : MonoBehaviour {

	// Merge Sort
	public static List<int> MergeSort(List<int> a) {
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

	static List<int> Merge(List<int> a, List<int> b) {
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

	// Insertion Sort
	public static List<int> InsertionSort(List<int> a) {
		int pos;
		int value;

		for (int i = 1; i < a.Count; i++) {
			value = a[i];
			pos = i;

			while (pos > 0 && a[pos - 1] > value) {
				a[pos] = a[pos - 1];
				pos--;
			}
			a[pos] = value;
		}
		return a;
	}
}
