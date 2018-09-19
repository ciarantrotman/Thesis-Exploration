using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererCurvedRadius : MonoBehaviour
{
	private LineRendererCurved _lr;
	private float _dist;
	
	public GameObject DistanceRef;

	private void Start ()
	{
		_lr = GetComponent<LineRendererCurved>();
	}

	private void Update ()
	{
		_dist = Vector3.Magnitude(transform.position - DistanceRef.transform.position);
		_lr.Radius = _dist;
	}
}