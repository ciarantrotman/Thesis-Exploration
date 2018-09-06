using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LookAtTrigger : MonoBehaviour
{
	public GameObject LookAtTarget;
	public float TriggerAngle;
	public Color DebugLineColor;
		
	public UnityEvent LookBegin;
	public UnityEvent LookStay;
	public UnityEvent LookEnd;

	private bool _switch;
	private float _currentAngle;

	private void Start()
	{
		_switch = false;
	}

	private void Update()
	{
		_currentAngle = Vector3.Angle(transform.forward, LookAtTarget.transform.forward);
		Debug.DrawLine(transform.position, LookAtTarget.transform.position, DebugLineColor);
		
		if (_currentAngle > TriggerAngle/2  && _switch == true)
			Invoke("OnLookEnd", 0);
		
		if (_currentAngle < TriggerAngle/2 && _switch == true)
			Invoke("OnLookStay", 0);
		
		if (_currentAngle < TriggerAngle/2 && _switch == false)
			Invoke("OnLookBegin", 0);
	}

	public void OnLookBegin()
	{
		_switch = true;
		LookBegin.Invoke();
	}
	public void OnLookStay()
	{
		
		LookStay.Invoke();
	}
	public void OnLookEnd()
	{
		_switch = false;
		LookEnd.Invoke();
	}
}
