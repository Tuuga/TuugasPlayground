using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyCode : MonoBehaviour {

	public int listCount;
	public List<int> myList;
	
	void Start () {
		myList = new List<int>(listCount);
		myList = ListTools.RandomizeList(myList);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			myList = Sorting.MergeSort(myList);
		}
	}
}
