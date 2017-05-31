using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class ThreadingTest : MonoBehaviour {

	public GameObject visual;

	Thread main;
	public List<int> myList = new List<int>();	

	void Start () {
		myList = ListTools.Ordered(100);
		myList = ListTools.RandomizeList(myList);

		main = new Thread(new ThreadStart(StartSort));
		main.Start();
	}

	void StartSort () {
		myList = MergeSort(myList);
		main.Abort();
	}

	// Merge Sort
	List<int> MergeSort(List<int> a) {
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

		print("Sleep 100");
		Thread.Sleep(100);

		l1 = MergeSort(l1);
		l2 = MergeSort(l2);

		print("Done");
		return Merge(l1, l2);
	}

	List<int> Merge(List<int> a, List<int> b) {
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
}
