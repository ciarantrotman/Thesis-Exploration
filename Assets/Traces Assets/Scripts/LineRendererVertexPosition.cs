using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererVertexPosition : MonoBehaviour 
{
	private LineRenderer _lineRenderer;

	public Transform VertexPosition;

	private void Start()
	{
		_lineRenderer = transform.GetComponent<LineRenderer>();
	}

	void Update () 
	{
		_lineRenderer.SetPosition(0, transform.position);
		_lineRenderer.SetPosition(1, VertexPosition.transform.position);
	}
}
