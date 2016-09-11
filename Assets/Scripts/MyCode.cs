using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyCode : MonoBehaviour {

	public int listCount;
	List<int> myList = new List<int>();
	
	void Start () {
		print("myList = ListTools.Ordered(listCount);");
		myList = ListTools.Ordered(listCount);
		ListTools.PrintList(myList);
		print("myList = ListTools.RandomizeList(myList);");
		myList = ListTools.RandomizeList(myList);
		ListTools.PrintList(myList, "<color=red>");
		print("myList = Sorting.MergeSort(myList);");
		myList = Sorting.MergeSort(myList);
		ListTools.PrintList(myList, "<color=green>");
	}
}
