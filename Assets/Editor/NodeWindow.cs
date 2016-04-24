using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class NodeWindow : EditorWindow {

	public GameObject nodeHolder;
	public GameObject node;
	public GameObject line;
	GameObject lineIns;
	LineRenderer _lr;
	GameObject _lastSpawnedNodeHolder;
	List<GameObject> _nodes;
	List<Vector3> _nodeCreationPos;
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

		SpawnNodesButton();
		DrawLineButton();
		
	}

	void Update () {
		if (_nodes != null && _lastSpawnedNodeHolder != null && _radius >= 0.1f) {
			Vector3 creationZeroY = Vector3.zero;
			for (int i = 0; i < _nodes.Count; i++) {
				creationZeroY = new Vector3(_nodeCreationPos[i].x, 0, _nodeCreationPos[i].z);
				_nodes[i].transform.position = (creationZeroY * _radius) + (Vector3.up * _layerHeight * _nodeCreationPos[i].y) + _lastSpawnedNodeHolder.transform.position;
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

	void SpawnNodesButton () {
		if (GUILayout.Button("Spawn Nodes")) {
			_lastSpawnedNodeHolder = (GameObject)Instantiate(nodeHolder, Vector3.zero, Quaternion.identity);
			_lastSpawnedNodeHolder.name = "NodeHolder";

			_nodes = new List<GameObject>();
			_nodeCreationPos = new List<Vector3>();
			for (int j = 0; j < _nodeLayerCount; j++) {
				for (int i = 0; i < _nodeCount / _nodeLayerCount; i++) {
					Vector3 nodePos = Vector3.forward + (Vector3.up * j);
					if (i > 0)
						nodePos = Quaternion.AngleAxis(360f / (_nodeCount / _nodeLayerCount) * i, Vector3.up) * nodePos;
					GameObject nodeIns = (GameObject)Instantiate(node, nodePos, Quaternion.identity);
					nodeIns.name = "Node";
					nodeIns.transform.parent = _lastSpawnedNodeHolder.transform;
					_nodes.Add(nodeIns);
					_nodeCreationPos.Add(nodeIns.transform.position);
				}
			}
		}
	}

	void DrawLineButton () {
		if (GUILayout.Button("Draw Line")) {
			if (lineIns == null)
				lineIns = Instantiate(line);
			_lr = lineIns.GetComponent<LineRenderer>();
			_lr.SetVertexCount(_nodes.Count);
			for (int i = 0; i < _nodes.Count; i++) {
				_lr.SetPosition(i, _nodes[i].transform.position);
			}
		}
	}
}