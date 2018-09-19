using System.Collections;
using System.Collections.Generic;
using Leap;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using TMPro;

public class IndirectManipulation : MonoBehaviour
{
    #region Variables
    public float LerpSpeed = .025f;

    private float _ctrlRat;
    
    private GameObject _user;
    private GameObject _ctrlMidpointRef;
    private GameObject _ctrlProx;
    private GameObject _ctrlAct;
    private GameObject _ctrlText;
    [HideInInspector]
    public GameObject _indProx;
    private GameObject _indAct;
    private GameObject _indText;
    private GameObject _ctrlRefNorm;
    private GameObject _indRefNorm;
    private GameObject _ctrlRatMax;
    private GameObject _indRatMax;
    private GameObject _indActScaled;
    private GameObject _objectText;
    
    private Vector3 _userPos;
    private Vector3 _displacment;
    private Vector3 _zDepthAdjust;
    
    [HideInInspector]
    public bool GraspState;
    
    private LineRenderer _ctrlLine;
    private LineRenderer _indLine;
    private LineRenderer _joinLine;
    
    private ObjectSelection _objectSelection;
    #endregion

    private void Start()
    {    
        _user = GameObject.Find("HMD Camera");
        _ctrlMidpointRef = GameObject.Find("CtrlMidpointRef");
        _ctrlProx = GameObject.Find("CtrlProx");
        _ctrlAct = GameObject.Find("CtrlAct");
        _ctrlText = GameObject.Find("CtrlText");
        _indProx = GameObject.Find("IndProx");
        _indAct = GameObject.Find("IndAct");
        _indText = GameObject.Find("IndText");
        _ctrlRefNorm = GameObject.Find("CtrlRefNorm");
        _indRefNorm = GameObject.Find("IndRefNorm");
        _ctrlRatMax = GameObject.Find("CtrlRatMax");
        _indRatMax = GameObject.Find("IndRatMax");
        _indActScaled = GameObject.Find("IndActScaled");
        _objectText = GameObject.Find("ActiveObjectText");
        
        _ctrlLine = _ctrlProx.GetComponent<LineRenderer>();
        _indLine = _indProx.GetComponent<LineRenderer>();
        _joinLine = transform.GetComponent<LineRenderer>();
        
        _objectSelection = _user.GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        if (_objectSelection.LastActiveObject == null) return;
        Invoke("FollowActiveProgram",0);
        
        if (GraspState != true) return;
        Invoke("DrawLineRenderers", 0);
        Invoke("WriteTextMeshPro", 0);
        Invoke("GraspStay", 0);

    }

    private void FollowActiveProgram()
    {
        if (GraspState != false || _objectSelection.LastActiveObject == null) return;
        
        var activeProgamPos = _objectSelection.LastActiveObject.transform.position;
        
        if (GameObject.Find("MultiSelectionLogic").GetComponent<MultiSelectionViewport>().MultiSelectionObjects.Count > 0)
        {
            // maybe figure out how to get the middle of a bunch of vectors?
            _indProx.transform.position = GameObject.Find("MultiSelectionLogic").GetComponent<MultiSelectionViewport>().MultiSelectionObjects[0].gameObject.transform.position;
            return;
        }
        
        _indProx.transform.position = activeProgamPos;
    }
    
    private void DrawLineRenderers()
    {
        _ctrlLine.SetPosition(0, _ctrlProx.transform.position);
        _ctrlLine.SetPosition(1, _ctrlAct.transform.position);
        _indLine.SetPosition(0, _indProx.transform.position);
        _indLine.SetPosition(1, _indActScaled.transform.position);
        _joinLine.SetPosition(0, _indActScaled.transform.position);
        _joinLine.SetPosition(1, _ctrlAct.transform.position);
    }

    private void WriteTextMeshPro()
    {
        var ctrlText = _ctrlText.GetComponent<TextMeshPro>();
        ctrlText.SetText("{0:2}", _ctrlRat);
        
        var indText = _indText.GetComponent<TextMeshPro>();
        indText.SetText("{0:2}", _indActScaled.transform.localPosition.z);
        
        var activeObject = _objectText.GetComponent<TextMeshPro>();
        activeObject.text = (_objectSelection.LastActiveObject.transform.name);
    }
    
    public void GraspBegin()
    {   
        if (_objectSelection.LastActiveObject == null) return;

        _ctrlLine.enabled = true;
        _indLine.enabled = true;
        _joinLine.enabled = true;
        
        var userPosx = (_ctrlMidpointRef.transform.position.x + _ctrlProx.transform.position.x) / 2;
        var userPosy = (_ctrlMidpointRef.transform.position.y + _ctrlProx.transform.position.y) / 2;
        var userPosz = (_ctrlMidpointRef.transform.position.z + _ctrlProx.transform.position.z) / 2;
        _userPos = new Vector3(userPosx, userPosy, userPosz);

        _indRatMax.transform.localPosition = _indRefNorm.transform.localPosition;
        _indRatMax.transform.localRotation = _indRefNorm.transform.localRotation;
        _ctrlRatMax.transform.localPosition = _ctrlRefNorm.transform.localPosition;
        _ctrlRatMax.transform.localRotation = _ctrlRefNorm.transform.localRotation;
        
        GraspState = true;

        if (GameObject.Find("MultiSelectionLogic").GetComponent<MultiSelectionViewport>().MultiSelectionObjects.Count > 0)
        {
            GameObject.Find("MultiSelectionLogic").GetComponent<MultiSelectionViewport>().Invoke("GrabBegin", 0);
            return;
        }
            
        if (_objectSelection.LastActiveObject.GetComponent<ObjectBehaviours>() != null)
            _objectSelection.LastActiveObject.GetComponent<ObjectBehaviours>().Invoke("OnGrabBegin",0);
    }

    public void GraspStay()
    {
        if (_objectSelection.LastActiveObject == null) return;
        
        
        _ctrlRat = _ctrlRefNorm.transform.localPosition.z / _ctrlRatMax.transform.localPosition.z;
        _indActScaled.transform.localPosition = _indRefNorm.transform.localPosition * (_ctrlRat*_ctrlRat);
        
        if (GameObject.Find("MultiSelectionLogic").GetComponent<MultiSelectionViewport>().MultiSelectionObjects.Count > 0)
        {
            GameObject.Find("MultiSelectionLogic").GetComponent<MultiSelectionViewport>().Invoke("GrabStay", 0);
            return;
        }
            
        _objectSelection.LastActiveObject.GetComponent<ObjectBehaviours>().Invoke("OnGrabStay",0);
    }
    
    public void GraspEnd()
    {
        if (_objectSelection.LastActiveObject == null) return;
        
        _ctrlLine.enabled = false;
        _indLine.enabled = false;
        _joinLine.enabled = false;
        

        GraspState = false;
        _indProx.transform.position = _indAct.transform.position;
        _ctrlProx.transform.position = _ctrlAct.transform.position;
        _indAct.transform.localPosition = new Vector3(0, 0, 0);
        _ctrlAct.transform.localPosition = new Vector3(0, 0, 0);
                
        if (GameObject.Find("MultiSelectionLogic").GetComponent<MultiSelectionViewport>().MultiSelectionObjects.Count > 0)
        {
            GameObject.Find("MultiSelectionLogic").GetComponent<MultiSelectionViewport>().Invoke("GrabEnd", 0);
            return;
        }
        
        if (_objectSelection.LastActiveObject.GetComponent<ObjectBehaviours>() != null)
            _objectSelection.LastActiveObject.GetComponent<ObjectBehaviours>().Invoke("OnGrabEnd",0);
    }
}
