using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using UnityEngine;

public class FollowLookAt : MonoBehaviour
{
	public bool LookAway = false;
	public GameObject TargetObject;
	public GameObject TargetLookAt;
	public Color DebugLineColor;
	
	void Update ()
	{
		transform.position = TargetObject.transform.position;
		if (LookAway == true)
			transform.LookAwayFrom(TargetLookAt.transform);
		else if (LookAway == false)
			transform.LookAt(TargetLookAt.transform);
		
		Debug.DrawLine(TargetObject.transform.position, TargetLookAt.transform.position, DebugLineColor);
	}
}
