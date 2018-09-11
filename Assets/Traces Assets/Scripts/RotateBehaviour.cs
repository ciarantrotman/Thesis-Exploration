using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBehaviour : MonoBehaviour 
{
	private GameObject _rotateTarget;
	private Quaternion _initTarRot;
	private Quaternion _initRot;
	
	public void RotateActionBegin()
	{
		_rotateTarget = GameObject.Find("HMD Camera").GetComponent<ObjectSelection>().LastActiveObject;
		_initTarRot = _rotateTarget.transform.rotation;
		_initRot = transform.rotation;
	}
    
	public void RotateActionStay()
	{
		var applyRot = (_initRot) * (transform.rotation);
		_rotateTarget.transform.rotation = _initTarRot * applyRot;
	}
}
