using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveProgramLineRenderer : MonoBehaviour
{
	private LineRenderer _lr;
	
	private void Start()
	{
		_lr = transform.GetComponent<LineRenderer>();
	}

	private void Update () 
	{
		_lr.positionCount = 2;
		_lr.SetPosition(0, transform.position);
		_lr.SetPosition(1,
			GameObject.Find("HMD Camera").GetComponent<ObjectSelection>().LastActiveObject != null
				? GameObject.Find("HMD Camera").GetComponent<ObjectSelection>().LastActiveObject.transform.position
				: transform.position);
	}
}
