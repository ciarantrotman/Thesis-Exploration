using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSelectionViewport : MonoBehaviour
{
	#region Variables
	private GameObject _self;
	private GameObject _user;
	private ObjectSelection _objectSelection;
	private bool _active;
	private Vector3 _topLeft;
	private Vector3 _bottomRight;
	private Vector3 _topRight;
	private Vector3 _bottomLeft;
	private GameObject _selectionTarget;
	private LineRenderer _lr;
	private GameObject _posXRef;
	private GameObject _posYRef;
	private GameObject _xAngRef;
	private GameObject _yAngRef;
	private GameObject _xRRef;
	private GameObject _xLRef;
	private GameObject _yTRef;
	private GameObject _yBRef;
	private GameObject _objProx;

	private Vector3 _topMidpoint;
	private Vector3 _leftMidpoint;
	private Vector3 _bottomMidpoint;
	private Vector3 _rightMidpoint;
	private Vector3 _centerMidpoint;

	private GameObject _grab;
	public List<GameObject> MultiSelectionObjects;
	private bool _pause;
	private GameObject _lerpTarget;
	private IndirectManipulation _indirectManipulation;
	
	private MeshFilter _mf;
	private Mesh _m;
	#endregion
	
	private void Start ()
	{
		_self = transform.gameObject;
		_user = GameObject.Find("HMD Camera"); 
		_objectSelection = _user.GetComponent<ObjectSelection>();
		_selectionTarget = GameObject.Find("MultiSelectTarget");
		_posXRef = GameObject.Find("MSObjXRef");
		_posYRef = GameObject.Find("MSObjYRef");
		_xAngRef = GameObject.Find("MSXAngRef");
		_yAngRef = GameObject.Find("MSYAngRef");
		_xRRef = GameObject.Find("MSXRRef");
		_xLRef = GameObject.Find("MSXLRef");
		_yTRef = GameObject.Find("MSYTRef");
		_yBRef = GameObject.Find("MSYBRef");
		_objProx = GameObject.Find("MSObjProx");
		_grab = GameObject.Find("MSIndAct");
		_lr = GetComponent<LineRenderer>();
		_lerpTarget = GameObject.Find("IndActScaled");
		_indirectManipulation = GameObject.Find("SceneController").GetComponent<IndirectManipulation>();
		_mf = GetComponent<MeshFilter>();
		_m = _mf.mesh;
	}

	private void Update()
	{
		if (_pause == true) return;
		
		if (_active == true)
			_bottomRight = _selectionTarget.transform.position;

		Invoke("Calculations",0);                                      
		Invoke("DrawLineRenderer",0);

		#region Vector Calculations
		var selfNorm = new Vector3(transform.position.x, 0, transform.position.z);
		var bottomRefNorm = new Vector3(_bottomMidpoint.x, 0, _centerMidpoint.z);
		var xRRef = new Vector3(_rightMidpoint.x, 0, _rightMidpoint.z);
		var xLRef = new Vector3(_leftMidpoint.x, 0, _leftMidpoint.z);
		
		transform.position = _user.transform.position;
		transform.LookAt(_centerMidpoint);
		
		_xAngRef.transform.position = selfNorm;
		_xAngRef.transform.LookAt(bottomRefNorm);

		_yAngRef.transform.position = transform.position;
		
		_xRRef.transform.position = selfNorm;
		_xRRef.transform.LookAt(xRRef);
		
		_xLRef.transform.position = selfNorm;
		_xLRef.transform.LookAt(xLRef);

		_yTRef.transform.position = transform.position;
		_yTRef.transform.LookAt(_topMidpoint);
		
		_yBRef.transform.position = transform.position;
		_yBRef.transform.LookAt(_bottomMidpoint);
		
		_posXRef.transform.position = selfNorm;
		
		var xRAng = Vector3.Angle(_xAngRef.transform.forward, _xRRef.transform.forward);
		var xLAng = Vector3.Angle(_xAngRef.transform.forward, _xLRef.transform.forward);
		
		var yTAng = Vector3.Angle(transform.forward, _yTRef.transform.forward);
		var yBAng = Vector3.Angle(transform.forward, _yBRef.transform.forward);
		
		Debug.DrawLine(_xAngRef.transform.position, bottomRefNorm, Color.red);
		Debug.DrawLine(_xRRef.transform.position, xRRef, Color.yellow);
		Debug.DrawLine(_xLRef.transform.position, xLRef, Color.yellow);
		#endregion
		
		if (MultiSelectionObjects.Count > 0)
			_grab.transform.position = MultiSelectionObjects[0].gameObject.transform.position;
		
		foreach (var selectableObject in _objectSelection.GlobalSelectableObjects)
		{
			_objProx.transform.position = selectableObject.transform.position;
			
			var objectXPosRefNorm = new Vector3(selectableObject.transform.position.x, 0, selectableObject.transform.position.z);
			var objectYPosRefNorm = new Vector3(0, _objProx.transform.localPosition.y, _objProx.transform.localPosition.z);
	
			_posXRef.transform.LookAt(objectXPosRefNorm); 
			
			_posYRef.transform.localPosition = objectYPosRefNorm;
			_yAngRef.transform.LookAt(_posYRef.transform.position);
			
			var objXRAng = Vector3.Angle(_posXRef.transform.forward, _xRRef.transform.forward);
			var objXLAng = Vector3.Angle(_posXRef.transform.forward, _xLRef.transform.forward);
			var objXCAng = Vector3.Angle(_posXRef.transform.forward, _xAngRef.transform.forward);
			
			var objYTAng = Vector3.Angle(_yAngRef.transform.forward, _yTRef.transform.forward);
			var objYBAng = Vector3.Angle(_yAngRef.transform.forward, _yBRef.transform.forward);
			var objYCAng = Vector3.Angle(_yAngRef.transform.forward, transform.forward);
			
			Debug.DrawRay(transform.position, transform.forward, Color.red);
			Debug.DrawRay(transform.position, _yTRef.transform.forward, Color.yellow);
			Debug.DrawRay(transform.position, _yBRef.transform.forward, Color.yellow);
	
			//Debug.DrawRay(transform.position, _yAngRef.transform.forward, Color.white);
			//Debug.Log(selectableObject.name+": "+Mathf.Round(objXCAng + objXLAng)+" = "+Mathf.Round(xLAng)+" / "+Mathf.Round(objXCAng + objXRAng)+" = "+Mathf.Round(xRAng));
			//Debug.Log(selectableObject.name+": "+Mathf.Round(objXCAng + objXLAng)+" = "+Mathf.Round(yTAng)+" / "+Mathf.Round(objXCAng + objYBAng)+" = "+Mathf.Round(yBAng));			
			
			if (Mathf.Approximately(Mathf.Round(objXCAng + objXLAng), Mathf.Round(xLAng)) || Mathf.Approximately(Mathf.Round(objXCAng + objXRAng), Mathf.Round(xRAng)))
			{
				if (Mathf.Approximately(Mathf.Round(objYCAng + objYTAng), Mathf.Round(yTAng)) || Mathf.Approximately(Mathf.Round(objYCAng + objYBAng), Mathf.Round(yBAng)))
				{
					if (MultiSelectionObjects.Contains(selectableObject) != false) continue;
					MultiSelectionObjects.Add(selectableObject);
					selectableObject.GetComponent<ObjectBehaviours>().MultiSelectBegin.Invoke();
				}
				else
				{
					if (MultiSelectionObjects.Contains(selectableObject) != true) continue;
					MultiSelectionObjects.Remove(selectableObject);
					selectableObject.GetComponent<ObjectBehaviours>().MultiSelectEnd.Invoke();
				}
			}
			else
			{
				if (MultiSelectionObjects.Contains(selectableObject) != true) continue;
				MultiSelectionObjects.Remove(selectableObject);
				selectableObject.GetComponent<ObjectBehaviours>().MultiSelectEnd.Invoke();
			}
		}
	}

	private void DrawLineRenderer()
	{
		if (_active == false) return;
		_lr.positionCount = 5;
		_lr.SetPosition(0, _topLeft);
		_lr.SetPosition(1, _topRight);
		_lr.SetPosition(2, _bottomRight);
		_lr.SetPosition(3, _bottomLeft);
		_lr.SetPosition(4, _topLeft);
		
		_m.vertices = new Vector3[]
		{
			_topLeft, 
			_topRight, 
			_bottomRight,
			_bottomLeft
		};
		_m.triangles = new int[]
		{
			0,2,3,
			3,1,0
		};
	}

	private void Calculations()
	{
		_topRight = new Vector3(_bottomRight.x, _topLeft.y, _bottomRight.z);
		_bottomLeft = new Vector3(_topLeft.x, _bottomRight.y, _topLeft.z);
		
		_topMidpoint = new Vector3((_topLeft.x+_topRight.x)/2, (_topLeft.y+_topRight.y)/2, (_topLeft.z+_topRight.z)/2);
		_leftMidpoint = new Vector3((_topLeft.x+_bottomLeft.x)/2, (_topLeft.y+_bottomLeft.y)/2, (_topLeft.z+_bottomLeft.z)/2);
		_bottomMidpoint = new Vector3((_bottomLeft.x+_bottomRight.x)/2, (_bottomLeft.y+_bottomRight.y)/2, (_bottomLeft.z+_bottomRight.z)/2);
		_rightMidpoint = new Vector3((_topRight.x+_bottomRight.x)/2, (_topRight.y+_bottomRight.y)/2, (_topRight.z+_bottomRight.z)/2);
		_centerMidpoint = new Vector3((_topMidpoint.x+_bottomMidpoint.x)/2, (_topMidpoint.y+_bottomMidpoint.y)/2, (_topMidpoint.z+_bottomMidpoint.z)/2);
	}
	
	public void MultiSelectionBegin()
	{
		_active = true;
		_topLeft = _selectionTarget.transform.position;
	}

	public void MultiSelectionEnd()
	{
		_active = false;
		_bottomRight = _selectionTarget.transform.position;
	}

	public void MultiSelectionClear()
	{
		_active = false;
		_lr.positionCount = 0;
		_topLeft = new Vector3(0,0,0);
		_bottomRight = new Vector3(0,0,0);
	}

	public void GrabBegin()
	{
		_pause = true;
		foreach (var selectableObject in MultiSelectionObjects)
		{
			selectableObject.transform.SetParent(_grab.transform);
		}
	}
	
	public void GrabStay()
	{
		_grab.transform.position = Vector3.Lerp(_grab.transform.position, _lerpTarget.transform.position, _indirectManipulation.LerpSpeed);
	}
	
	public void GrabEnd()
	{
		_pause = false;
		foreach (var selectableObject in MultiSelectionObjects)
		{
			selectableObject.transform.SetParent(selectableObject.GetComponent<ObjectBehaviours>().OriginalParent);
		}
	}
}
