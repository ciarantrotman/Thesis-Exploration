using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBehaviour : MonoBehaviour 
{
	private LineRenderer _lineRenderer;

	private GameObject _controlNode;

	private void Start()
	{
		_lineRenderer = transform.GetComponent<LineRenderer>();
		_controlNode = GameObject.Find("ControlNode");
	}

	void Update () 
	{
		_lineRenderer.useWorldSpace = true;
		_lineRenderer.SetWidth(0.0015f, 0.0015f);
		_lineRenderer.SetVertexCount(2);
		_lineRenderer.SetPosition(0, transform.position);
		_lineRenderer.SetPosition(1, _controlNode.transform.position);
	}
}
