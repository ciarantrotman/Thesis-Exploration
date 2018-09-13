using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class LineGizmo : MonoBehaviour
{
	public Transform Target;
	public Color LineColor;

	private void Update () 
	{
		if (Target != null)
			Debug.DrawLine(transform.position, Target.transform.position, LineColor);
	}
}
