using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class NodeWindow : EditorWindow {

	NodeShape.NodeArrayShape _nodeShape;

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

		CreateShapeSelector("Node Array Shape", ref _nodeShape);
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
			_nodeHolderScript.SetShape(_nodeShape);

			if (_nodeHolderScript.GetShape() == NodeShape.NodeArrayShape.Cylinder) {
				CylinderModify();
			} else if (_nodeHolderScript.GetShape() == NodeShape.NodeArrayShape.Cube) {
				CubeModify();
			} else if (_nodeHolderScript.GetShape() == NodeShape.NodeArrayShape.Sphere) {
				SphereModify();
			}
		}		
	}

	void CylinderModify () {
		if (_selectedNodes != null && _selected != null && _radius >= 0.1f) {
			Vector3 creationZeroY = Vector3.zero;
			for (int i = 0; i < _selectedNodes.Count; i++) {
				creationZeroY = new Vector3(_selectedNodeStartPos[i].x, 0, _selectedNodeStartPos[i].z);
				_selectedNodes[i].transform.position = (creationZeroY * _radius) + (Vector3.up * _layerHeight * _selectedNodeStartPos[i].y) + _selected.transform.position;
			}
		}
	}

	void CubeModify () {

	}

	void SphereModify () {

	}

	void SpawnNodesButton () {
		if (GUILayout.Button("Spawn Nodes")) {
			GameObject _nodeHolderIns = (GameObject)Instantiate(nodeHolder, Vector3.zero, Quaternion.identity);
			_nodeHolderIns.name = "NodeHolder";
			_nodeHolderScript = _nodeHolderIns.GetComponent<NodeHolder>();

			List<GameObject> createdNodes = new List<GameObject>();
			List<Vector3> createdNodesPos = new List<Vector3>();

			if (_nodeShape == NodeShape.NodeArrayShape.Cylinder) {
				SpawnCylinderNodeArray(_nodeHolderIns.transform, ref createdNodes, ref createdNodesPos);
			} else if (_nodeShape == NodeShape.NodeArrayShape.Cube) {
				SpawnCubeNodeArray(_nodeHolderIns.transform, ref createdNodes, ref createdNodesPos);
			} else if (_nodeShape == NodeShape.NodeArrayShape.Sphere) {
				SpawnSphereNodeArray(_nodeHolderIns.transform, ref createdNodes, ref createdNodesPos);
			}

			_nodeHolderScript.SetNodes(createdNodes, createdNodesPos);
			_nodeHolderScript.SetShape(_nodeShape);
		}
	}

	void InitializeNode (Vector3 nodePos , Transform nodeHolderTransform, ref List<GameObject> createdNodes, ref List<Vector3> createdNodesPos) {
		GameObject nodeIns = (GameObject)Instantiate(node, nodePos, Quaternion.identity);
		nodeIns.name = "Node";
		nodeIns.transform.parent = nodeHolderTransform;
		createdNodes.Add(nodeIns);
		createdNodesPos.Add(nodeIns.transform.position);
	}

	void SpawnCylinderNodeArray (Transform nodeHolderTransform, ref List<GameObject> createdNodes, ref List<Vector3> createdNodesPos) {
		
		for (int j = 0; j < _nodeLayerCount; j++) {
			for (int i = 0; i < _nodeCount / _nodeLayerCount; i++) {
				Vector3 nodePos = Vector3.forward + (Vector3.up * j);
				if (i > 0)
					nodePos = Quaternion.AngleAxis(360f / (_nodeCount / _nodeLayerCount) * i, Vector3.up) * nodePos;
				InitializeNode(nodePos, nodeHolderTransform, ref createdNodes, ref createdNodesPos);
			}
		}
	}

	void SpawnCubeNodeArray (Transform nodeHolderTransform, ref List<GameObject> createdNodes, ref List<Vector3> createdNodesPos) {

	}

	void SpawnSphereNodeArray (Transform nodeHolderTransform, ref List<GameObject> createdNodes, ref List<Vector3> createdNodesPos) {

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

	void CreateShapeSelector (string prefix, ref NodeShape.NodeArrayShape value) {
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel(prefix);
		value = (NodeShape.NodeArrayShape)EditorGUILayout.EnumPopup(_nodeShape);
		EditorGUILayout.EndHorizontal();
	}
}