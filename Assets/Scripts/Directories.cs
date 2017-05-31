using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dir {
	public string name;
	public string path;
	public Dir parent;
	public List<Dir> childs = new List<Dir>();
	public List<string> files = new List<string>();

	public Dir (string name, Dir parent) {
		this.name = name;
		this.parent = parent;
		if (parent != null) {
			parent.childs.Add(this);
		}
		path = Directories.FromRoot(this);
	}

	public void AddFile (string name, string text) {
		var filePath = path + "\\" + name;
		if (System.IO.File.Exists(filePath)) { return; }

		System.IO.Directory.CreateDirectory(path);
		System.IO.File.WriteAllText(filePath, text);
		files.Add(name);
	}
}

public class Directories : MonoBehaviour {

	static string root = "f:\\DirTest";
	public string findPathToFile;
	public int dirCount;
	public int maxFileCountPerDir;
	Dir rootDir;
	Dictionary<string, string> nameToPath = new Dictionary<string, string>();

	void Start () {
		rootDir = new Dir("root", null);
		StartCoroutine(Generate());
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			var pathsw = new System.Diagnostics.Stopwatch();
			pathsw.Start();
			var path = PathToFile(findPathToFile, rootDir);
			print("Finding: " + pathsw.Elapsed);
			pathsw.Stop();
			print(path);
		}

		if (Input.GetKeyDown(KeyCode.W)) {
			var pathsw = new System.Diagnostics.Stopwatch();
			pathsw.Start();
			var path = nameToPath[findPathToFile];
			print("Finding: " + pathsw.Elapsed);
			pathsw.Stop();
			print(path);
		}
	}

	IEnumerator Generate () {
		var dirs = new List<Dir>();
		dirs.Add(rootDir);

		var gensw = new System.Diagnostics.Stopwatch();
		gensw.Start();

		int count = 0;
		for (int i = 0; i < dirCount; i++) {
			var parent = dirs[Random.Range(0, dirs.Count)];
			var dirName = i.ToString();
			var d = new Dir(dirName, parent);
			dirs.Add(d);
			var fileCount = Random.Range(0, maxFileCountPerDir);
			for (int y = 0; y < fileCount; y++) {
				var fileName = count.ToString() + ".txt";
				d.AddFile(fileName, GetRandomString(20));
				nameToPath.Add(fileName, d.path + "\\" + fileName);
				count++;
			}

			for (int c = 0; c < 100; c++) {
				if (c == 0) {
					print(d.path);
					yield return new WaitForEndOfFrame();
				}
			}
		}

		print("Generating: " + gensw.ElapsedMilliseconds + " ms");

		var files = dirs[Random.Range(0, dirs.Count)].files;
		findPathToFile = files[Random.Range(0, files.Count)];
	}

	string GetRandomString (int length) {
		var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_-";
		var stringChars = new char[length];

		for (int i = 0; i < stringChars.Length; i++) {
			var index = Random.Range(0, chars.Length);
			stringChars[i] = chars[index];
		}
		return new string(stringChars);
	}

	string PathToFile (string fileName, Dir from) {
		var s = new Stack<Dir>();
		var discovered = new Dictionary<Dir, Dir>();
		discovered.Add(from, null);
		var disc = new List<Dir>();

		s.Push(from);
		while (s.Count > 0) {
			var current = s.Pop();
			if (current.files.Contains(fileName)) {
				return current.path + "\\" + fileName;
			}

			if (!disc.Contains(current)) {
				disc.Add(current);
				foreach(Dir d in current.childs) {
					s.Push(d);
					if (!discovered.ContainsKey(d)) {
						discovered.Add(d, current);
					}
				}
			}
		}
		return "NULL";
	}

	public static string FromRoot (Dir d) {
		string location = root;
		var stack = new Stack<Dir>();
		stack.Push(d);
		while(d.parent != null) {
			d = d.parent;
			stack.Push(d);
		}
		while (stack.Count > 0) {
			location += "\\" + stack.Pop().name;
		}
		return location;
	}
}
