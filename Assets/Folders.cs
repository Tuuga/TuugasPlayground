using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct DirL {
	public List<DirR> folder;
	public string name;

	public DirL(string s) {
		folder = new List<DirR>();
		name = s;
	}
};

public struct DirR {
	public List<DirL> folder;
	public string name;

	public DirR(string s) {
		folder = new List<DirL>();
		name = s;
	}
};

public class Folders : MonoBehaviour {
	DirL main = new DirL("Main");
	void Start () {
		main.folder.Add(new DirR("1"));
		main.folder.Add(new DirR("2"));
		main.folder[0].folder.Add(new DirL("1.1"));
		main.folder[1].folder.Add(new DirL("2.1"));

		print(main.name);
		print(main.folder[0].name);
		print(main.folder[1].name);
		print(main.folder[0].folder[0].name);
		print(main.folder[1].folder[0].name);
	}
}
