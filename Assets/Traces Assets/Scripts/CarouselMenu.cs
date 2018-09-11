using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarouselMenu : MonoBehaviour
{
	public float RatchetAngle;
	public float ScrollSpeed;

	private bool _scrollCW;
	private bool _scrollCCW;
	private float _time;
	private GameObject _target;

	private void Start()
	{
		_target = GameObject.Find("MenuCarouselTarget");
	}

	private void Update ()
	{
		if (GameObject.FindGameObjectWithTag("LeftHandFingerController").GetComponent<FingerTriggerController>().Touching == false)
		{
			_scrollCW = false;
			_scrollCCW = false;
		}
		
		transform.localRotation = Quaternion.Slerp(transform.localRotation, _target.transform.localRotation, _time);
		_time = _time + Time.deltaTime;
		
		if (GameObject.FindGameObjectWithTag("LeftHandFingerController").GetComponent<FingerTriggerController>().Touching == false) return; 
		if (_scrollCW == true)
		{
			_target.transform.Rotate(Vector3.right * (Time.deltaTime*ScrollSpeed));
		}
		if (_scrollCCW == true)
		{
			_target.transform.Rotate(Vector3.left * (Time.deltaTime*ScrollSpeed));
		}
	}

	public void RatchetCW()
	{
		_target.transform.localRotation = Quaternion.Euler(_target.transform.localRotation.x + RatchetAngle, 0f, 0f);
		_time = 0;
	}

	public void RatchetCCW()
	{
		_target.transform.localRotation = Quaternion.Euler(_target.transform.localRotation.x - RatchetAngle, 0f, 0f);
		_time = 0;
	}

	public void ScrollCW()
	{
		_scrollCW = true;
		_time = 0;
	}

	public void ScrollCCW()
	{
		_scrollCCW = true;
		_time = 0;
	}
}
