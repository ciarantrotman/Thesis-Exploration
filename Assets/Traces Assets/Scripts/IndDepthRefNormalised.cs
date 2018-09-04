using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndDepthRefNormalised : MonoBehaviour 
{
	private Vector3 _defaultPosition;
	public GameObject TargetObject;

	private void Awake()
	{
		_defaultPosition = transform.localPosition;
	}
	
	void Update () 
	{
		transform.localPosition = new Vector3(_defaultPosition.x , _defaultPosition.y, TargetObject.transform.localPosition.z);
	}
}
