using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLerp : MonoBehaviour
{
	public Transform LerpTarget;
	public float LerpSpeed;

	private void Update () 
	{
		transform.position = Vector3.Lerp(transform.position, LerpTarget.transform.position, LerpSpeed);
		transform.rotation = Quaternion.Lerp(transform.rotation, LerpTarget.transform.rotation, LerpSpeed);
	}
}
