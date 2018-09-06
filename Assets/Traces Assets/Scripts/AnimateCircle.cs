using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AnimateCircle : MonoBehaviour
{
	public float StartAngle;
	public float EndAngle;
	public float AnimateTime;
	private float _currentTime;
	private bool _switch;

	private void Update ()
	{
		if (_switch == false) return;	
		if (_currentTime <= AnimateTime)
		{
			_currentTime += Time.deltaTime;
			GetComponent<LineRendererCurved>().EndAngle = Mathf.Lerp(StartAngle, EndAngle, _currentTime/AnimateTime);
		}
		else
		{	
			_currentTime = 0f;
			_switch = false;
		}
	}

	public void AnimateBegin()
	{
		_switch = true;
	}

	public void AnimateEnd()
	{
		GetComponent<LineRendererCurved>().EndAngle = StartAngle;
	}
}
