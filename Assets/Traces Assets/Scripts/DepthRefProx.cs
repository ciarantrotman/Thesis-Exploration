using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthRefProx : MonoBehaviour
{
	public GameObject TargetObject;
	public GameObject TargetLookAt;

	public Color DebugLineColor;
	
	private Vector3 _defaultRotation;
	
	void Update ()
	{
		transform.position = TargetObject.transform.position;
		transform.LookAt(TargetLookAt.transform);
		
		var rotation = transform.eulerAngles;
		transform.eulerAngles = new Vector3(_defaultRotation.x, rotation.y, _defaultRotation.z);
		
		Debug.DrawLine(TargetObject.transform.position, TargetLookAt.transform.position, DebugLineColor);
	}
}
