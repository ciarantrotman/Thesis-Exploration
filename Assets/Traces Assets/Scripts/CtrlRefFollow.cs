using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlRefFollow : MonoBehaviour
{

	private GameObject _user;
	private GameObject _ctrlIndRef;
	private Vector3 _ctrlRef;

	private void Start()
	{
		_user = GameObject.Find("HMD Camera");
		_ctrlIndRef = GameObject.Find("CtrlIndRef");
	}

	void Update ()
	{
		float x = _user.transform.position.x;
		float y = _ctrlIndRef.transform.position.y;
		float z = _user.transform.position.z;
		
		transform.position = new Vector3(x, y, z);
	}
}
