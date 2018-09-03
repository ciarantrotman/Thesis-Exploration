using System.Collections;
using System.Collections.Generic;
using Leap;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using TMPro;

public class IndirectManipulation : MonoBehaviour
{
    private GameObject _user;
    private GameObject _ctrlMidpointRef;
    private GameObject _ctrlProx;
    private GameObject _ctrlAct;
    private GameObject _ctrlText;
    private GameObject _indProx;
    private GameObject _indAct;
    private GameObject _indText;

    private Vector3 _userPos;

    [HideInInspector]
    public bool GraspState;
    
    private LineRenderer _ctrlLine;
    private LineRenderer _indLine;
    private LineRenderer _joinLine;
    
    private ObjectSelection _objectSelection;
    

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

        _ctrlLine = _ctrlProx.GetComponent<LineRenderer>();
        _indLine = _indProx.GetComponent<LineRenderer>();
        _joinLine = transform.GetComponent<LineRenderer>();
        
        _objectSelection = _user.GetComponent<ObjectSelection>();
    }

    private void Update()
    {
        Invoke("DrawLineRenderers", 0);
        Invoke("WriteTextMeshPro", 0);
        Invoke("FollowActiveProgram",0);
        
        /*
        Vector3 ctrlDirection = _ctrlAct.transform.position - _ctrlProx.transform.position;
        float ctrlDistance = ctrlDirection.magnitude;
        
        Vector3 indDirection = _indAct.transform.position - _indProx.transform.position;
        float indDistance = indDirection.magnitude;
        
        float userDistance = Vector3.Distance(_indProx.transform.position, _user.transform.position);
        float scaleFactor = Mathf.Pow(userDistance, 1.5f);

        if (_graspState == true)
            _indAct.transform.localPosition = (ctrlDirection*scaleFactor);
        */

        Vector3 ctrlDirection = _ctrlAct.transform.position - _userPos;
        float ctrlDistance = ctrlDirection.magnitude;
        
        Vector3 indDirection = _indAct.transform.position - _userPos;
        float indDistance = indDirection.magnitude;
        
        float ctrlDisplacement = Vector3.Magnitude(_ctrlAct.transform.position - _ctrlProx.transform.position);
        float scaleFactor = 1;//(ctrlDisplacement);

        if (GraspState == true)
        {
            _indAct.transform.localPosition = (ctrlDirection * .5f);//scaleFactor);
            Invoke("GraspStay", 0);
        }
            
    }

    private void FollowActiveProgram()
    {
        Vector3 activeProgamPos = _objectSelection.LastActiveObject.transform.position;
        _indProx.transform.position = activeProgamPos;
    }
    
    private void DrawLineRenderers()
    {
        _ctrlLine.SetPosition(0, _ctrlProx.transform.position);
        _ctrlLine.SetPosition(1, _ctrlAct.transform.position);
        _indLine.SetPosition(0, _indProx.transform.position);
        _indLine.SetPosition(1, _indAct.transform.position);
        _joinLine.SetPosition(0, _indAct.transform.position);
        _joinLine.SetPosition(1, _ctrlAct.transform.position);
    }

    private void WriteTextMeshPro()
    {
        TextMeshPro ctrlText = _ctrlText.GetComponent<TextMeshPro>();
        ctrlText.SetText("{0:1} : {1:1} : {2:1}", _ctrlAct.transform.localPosition.x, _ctrlAct.transform.localPosition.y, _ctrlAct.transform.localPosition.z);
        
        TextMeshPro indText = _indText.GetComponent<TextMeshPro>();
        indText.SetText("{0:1} : {1:1} : {2:1}", _indAct.transform.localPosition.x, _indAct.transform.localPosition.y, _indAct.transform.localPosition.z);
    }
    
    public void GraspBegin()
    {
        float userPosx = (_ctrlMidpointRef.transform.position.x + _ctrlProx.transform.position.x) / 2;
        float userPosy = (_ctrlMidpointRef.transform.position.y + _ctrlProx.transform.position.y) / 2;
        float userPosz = (_ctrlMidpointRef.transform.position.z + _ctrlProx.transform.position.z) / 2;
        _userPos = new Vector3(userPosx, userPosy, userPosz);
        GraspState = true;
    }

    public void GraspStay()
    {
        
    }
    
    public void GraspEnd()
    {
        GraspState = false;
        
        _indProx.transform.position = _indAct.transform.position;
        _ctrlProx.transform.position = _ctrlAct.transform.position;
        _indAct.transform.localPosition = new Vector3(0, 0, 0);
        _ctrlAct.transform.localPosition = new Vector3(0, 0, 0);
    }
}
