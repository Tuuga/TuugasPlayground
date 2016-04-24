using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class NodeWindow : EditorWindow {

	public GameObject nodeHolder;
	public GameObject node;
	public GameObject line;

	bool _useLineButton;
	LineRenderer _lr;

	NodeHolder _nodeHolderScript;
	GameObject _selected;
	List<GameObject> _selectedNodes;
	List<Vector3> _selectedNodeStartPos;
	int _nodeCount;
	int _nodeLayerCount;
	float _layerHeight;
	float _radius = 2f;

	[MenuItem("Window/Node Window")]
	static void Init () {
		NodeWindow window = (NodeWindow)EditorWindow.GetWindow(typeof(NodeWindow));
		window.Show();
	}

	void OnGUI () {
		CreateSlider("Node Count", ref _nodeCount, 2, 10000);
		CreateSlider("Node Layer Count", ref _nodeLayerCount, 1, _nodeCount / 2);
		EditorGUILayout.LabelField("Nodes per layer" , label2: "" + _nodeCount / _nodeLayerCount);
		CreateSlider("Node Layer Height", ref _layerHeight, 0.1f, 10f);
		CreateSlider("Node Radius", ref _radius, 0.1f, 100f);

		CreateToggle("Use Line Button", ref _useLineButton);

		if (_useLineButton)
			DrawLineButton();

		SpawnNodesButton();
		
	}

	void Update () {
		if (!_useLineButton)
			DrawLine();

		if (Selection.activeGameObject != null && Selection.activeGameObject.name == "NodeHolder") {
			_selected = Selection.activeGameObject;
			_nodeHolderScript = _selected.GetComponent<NodeHolder>();
			_selectedNodes = _nodeHolderScript.GetNodes();
			_selectedNodeStartPos = _nodeHolderScript.GetStartPos();
		}

		if (_selectedNodes != null && _selected != null && _radius >= 0.1f) {
			Vector3 creationZeroY = Vector3.zero;
			for (int i = 0; i < _selectedNodes.Count; i++) {
				creationZeroY = new Vector3(_selectedNodeStartPos[i].x, 0, _selectedNodeStartPos[i].z);
				_selectedNodes[i].transform.position = (creationZeroY * _radius) + (Vector3.up * _layerHeight * _selectedNodeStartPos[i].y) + _selected.transform.position;
			}
		}
	}

	void CreateSlider (string prefix, ref int value, int min, int max) {
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel(prefix);
		value = EditorGUILayout.IntSlider(value, min, max);
		EditorGUILayout.EndHorizontal();
	}

	void CreateSlider (string prefix, ref float value, float min, float max) {
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel(prefix);
		value = EditorGUILayout.Slider(value, min, max);
		EditorGUILayout.EndHorizontal();
	}

	void CreateToggle (string prefix, ref bool value) {
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel(prefix);
		value = EditorGUILayout.Toggle(value);
		EditorGUILayout.EndHorizontal();
	}

	void SpawnNodesButton () {
		if (GUILayout.Button("Spawn Nodes")) {
			GameObject _nodeHolderIns = (GameObject)Instantiate(nodeHolder, Vector3.zero, Quaternion.identity);
			_nodeHolderIns.name = "NodeHolder";
			_nodeHolderScript = _nodeHolderIns.GetComponent<NodeHolder>();

			List<GameObject> createdNodes = new List<GameObject>();
			List<Vector3> createdNodesPos = new List<Vector3>();

			for (int j = 0; j < _nodeLayerCount; j++) {
				for (int i = 0; i < _nodeCount / _nodeLayerCount; i++) {
					Vector3 nodePos = Vector3.forward + (Vector3.up * j);
					if (i > 0)
						nodePos = Quaternion.AngleAxis(360f / (_nodeCount / _nodeLayerCount) * i, Vector3.up) * nodePos;
					GameObject nodeIns = (GameObject)Instantiate(node, nodePos, Quaternion.identity);
					nodeIns.name = "Node";
					nodeIns.transform.parent = _nodeHolderIns.transform;
					createdNodes.Add(nodeIns);
					createdNodesPos.Add(nodeIns.transform.position);
				}
			}
			_nodeHolderScript.SetNodes(createdNodes, createdNodesPos);
		}
	}

	void DrawLine () {
		if (_selected != null) {
			GameObject lineIns = null;
			if (_selected.transform.Find("Line") != null) {
				lineIns = _selected.transform.Find("Line").gameObject;
			} else {
				lineIns = Instantiate(line);
				lineIns.name = "Line";
				lineIns.transform.parent = _selected.transform;
			}

			_lr = lineIns.GetComponent<LineRenderer>();
			_lr.SetVertexCount(_selectedNodes.Count);

			for (int i = 0; i < _selectedNodes.Count; i++) {
				_lr.SetPosition(i, _selectedNodes[i].transform.position);
			}
		}
	}

	void DrawLineButton () {
		if (GUILayout.Button("Draw Line")) {
			if (_selected != null) {
				GameObject lineIns = null;
				if (_selected.transform.Find("Line") != null) {
					lineIns = _selected.transform.Find("Line").gameObject;
				} else {
					lineIns = Instantiate(line);
					lineIns.name = "Line";
					lineIns.transform.parent = _selected.transform;
				}

				_lr = lineIns.GetComponent<LineRenderer>();
				_lr.SetVertexCount(_selectedNodes.Count);

				for (int i = 0; i < _selectedNodes.Count; i++) {
					_lr.SetPosition(i, _selectedNodes[i].transform.position);
				}
			}
		}
	}
}