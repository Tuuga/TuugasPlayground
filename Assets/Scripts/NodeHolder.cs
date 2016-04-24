using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeShape {
	public enum NodeArrayShape { Cylinder, Cube, Sphere }
}

public class NodeHolder : MonoBehaviour {

	public NodeShape.NodeArrayShape _currentShape;

	List<GameObject> _nodes;
	List<Vector3> _nodeStartPos;

	public List<GameObject> GetNodes () {
		return _nodes;
	}

	public List<Vector3> GetStartPos () {
		return _nodeStartPos;
	}

	public void SetNodes (List<GameObject> newNodes, List<Vector3> newNodeStartPos) {
		_nodes = newNodes;
		_nodeStartPos = newNodeStartPos;
	}

	public NodeShape.NodeArrayShape GetShape () {
		return _currentShape;
	}

	public void SetShape (NodeShape.NodeArrayShape newShape) {
		_currentShape = newShape;
	}
}
