using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour 
{
	public GameObject TargetObject;

	void Update () 
	{
		transform.position = TargetObject.transform.position;
		transform.rotation = TargetObject.transform.rotation;
	}
}
