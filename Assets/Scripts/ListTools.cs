using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListTools : MonoBehaviour {

	// List To String
	public static string ListToString(List<int> l) {
		string s = "";
		for (int i = 0; i <= 400; i++) {
			s += l[i] + " ";
		}
		return s;
	}

	// Create Ordered List
	public static List<int> Ordered (int count) {
		List<int> ord = new List<int>();
		
		for (int i = 0; i <= count; i++) {
			ord.Add(i);
		}
		return ord;
	}

	// RandomizeList
	public static List<int> RandomizeList(List<int> l) {
		for (int i = 0; i < l.Count; i++) {

			int randomInt = Random.Range(0, l.Count);
			int temp = l[randomInt];

			l[randomInt] = l[i];
			l[i] = temp;
		}
		return l;
	}

	// Print List
	public static void PrintList(List<int> l) {
		for (int i = 0; i < l.Count; i++) {
			print(l[i]);
		}
	}
	public static void PrintList(List<int> l, string tag) {
		for (int i = 0; i < l.Count; i++) {
			print(tag + l[i] + "</color>");
		}
	}
}
