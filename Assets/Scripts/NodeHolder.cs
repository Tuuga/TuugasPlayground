using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeHolder : MonoBehaviour {

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
}
