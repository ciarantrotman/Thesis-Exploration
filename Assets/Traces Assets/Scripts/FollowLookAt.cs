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

	private void Update ()
	{
		Debug.DrawLine(transform.position, TargetLookAt.transform.position, DebugLineColor);
		switch (LookAway)
		{
			case true:
				transform.LookAwayFrom(TargetLookAt.transform);
				break;
			case false:
				transform.LookAt(TargetLookAt.transform);
				break;
			default:
				break;
		}
		if (TargetObject == null) return;
			transform.position = TargetObject.transform.position;
	}
}
