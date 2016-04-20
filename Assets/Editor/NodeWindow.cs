using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class NodeWindow : EditorWindow {

	public GameObject nodeHolder;
	public GameObject node;
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

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Node Count");
		_nodeCount = EditorGUILayout.IntSlider(_nodeCount, 2, 100);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Node Layer Count");
		_nodeLayerCount = EditorGUILayout.IntSlider(_nodeLayerCount, 1, _nodeCount / 2);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Node Layer Height");
		_layerHeight = EditorGUILayout.Slider(_layerHeight, 0.1f, 100f);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Node Radius");
		_radius = EditorGUILayout.Slider(_radius, 0.1f, 100f);
		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("Spawn Nodes")) {
			_lastSpawnedNodeHolder = (GameObject)Instantiate(nodeHolder, Vector3.zero, Quaternion.identity);
			_lastSpawnedNodeHolder.name = "NodeHolder";

			_nodes = new List<GameObject>();
			_nodeCreationPos = new List<Vector3>();
			for (int j = 0; j < _nodeLayerCount; j++) {
				for (int i = 0; i < _nodeCount; i++) {
					Vector3 nodePos = Vector3.forward + (Vector3.up * j * _layerHeight);
					if (i > 0)
						nodePos = Quaternion.AngleAxis(360f / (_nodeCount /*/ _nodeLayers*/) * i, Vector3.up) * nodePos;
					GameObject nodeIns = (GameObject)Instantiate(node, nodePos, Quaternion.identity);
					nodeIns.name = "Node";
					nodeIns.transform.parent = _lastSpawnedNodeHolder.transform;
					_nodes.Add(nodeIns);
					_nodeCreationPos.Add(nodeIns.transform.position);
				}
			}
		}
	}

	void Update () {
		if (_nodes != null && _lastSpawnedNodeHolder != null && _radius >= 0.1f) {
			Vector3 creationZeroY = Vector3.zero;
			for (int i = 0; i < _nodes.Count; i++) {
				if (_nodes[i].transform.position.magnitude <= 100f)
					creationZeroY = new Vector3(_nodeCreationPos[i].x, 0, _nodeCreationPos[i].z);
					_nodes[i].transform.position = (creationZeroY * _radius) + (Vector3.up * _layerHeight * _nodeCreationPos[i].y);
			}
		}
	}
}