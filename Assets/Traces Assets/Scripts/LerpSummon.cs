using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpSummon : MonoBehaviour 
{
	private float _currentTime = 0f;
	public float TimeToMove = 2f;
	public Transform Target;
	public Transform FixedOrigin;
	private bool _summon;
	private bool _unsummon;
	private Vector3 _sumOrigPos;
	private Vector3 _unsumOrigPos;
	private Quaternion _sumOrigRot;
	private Quaternion _unsumOrigRot;

	private Vector3 _sumToPos;
	
	private void Update()
	{
		if (_summon == true)
		{
			if (_currentTime <= TimeToMove)
			{
				_currentTime += Time.deltaTime;
				transform.position = Vector3.Lerp(FixedOrigin.transform.position, _sumToPos, _currentTime / TimeToMove);
				transform.rotation = Quaternion.Lerp(FixedOrigin.transform.rotation, Target.transform.rotation, TimeToMove);
			}
			else
			{
				transform.position = transform.position;
				transform.rotation = transform.rotation;
				_currentTime = 0f;
				_summon = false;
			}
		}
		if (_unsummon == true)
		{
			if (_currentTime <= TimeToMove)
			{
				_currentTime += Time.deltaTime;
				transform.position = Vector3.Lerp(_unsumOrigPos, FixedOrigin.transform.position, _currentTime / TimeToMove);
				transform.rotation = Quaternion.Lerp(_unsumOrigRot, FixedOrigin.transform.rotation, TimeToMove);
			}
			else
			{
				transform.position = transform.position;
				transform.rotation = transform.rotation;
				_currentTime = 0f;
				_unsummon = false;
			}
		}
	}

	public void Summon()
	{
		if (transform.GetComponent<Rigidbody>() != null)
			transform.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		_summon = true;
		_sumOrigPos = transform.position;
		_sumOrigRot = transform.rotation;
		_sumToPos = Target.transform.position;
	}

	public void Unsummon()
	{
		if (transform.GetComponent<Rigidbody>() != null)
			transform.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
		_unsummon = true;
		_unsumOrigPos = transform.position;
		_unsumOrigRot = transform.rotation;
	}
}
