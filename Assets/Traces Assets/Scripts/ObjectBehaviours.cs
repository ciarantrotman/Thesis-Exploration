using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectBehaviours : MonoBehaviour
{
    private GameObject _self;
    private GameObject _user;
    private GameObject _microSelectionOrigin;
    private GameObject _lerpTarget;
    private ObjectSelection _objectSelection;
    private IndirectManipulation _indirectManipulation;
    private LineRenderer _lineRenderer;

    private int _stateCounter;
    
    public float MacroAngle;
    public float MicroAngle;

    public UnityEvent SelectBegin;
    public UnityEvent SelectEnd;
    public UnityEvent GrabBegin;
    public UnityEvent GrabEnd;
   
    private void Start()
    {
        _self = transform.gameObject;
        _user = GameObject.Find("HMD Camera");
        _microSelectionOrigin = GameObject.Find("MicroSelectionOrigin");
        _lerpTarget = GameObject.Find("IndActScaled");
        _objectSelection = _user.GetComponent<ObjectSelection>();
        _indirectManipulation = GameObject.Find("SceneController").GetComponent<IndirectManipulation>(); 
    }

    private void Update()
    {
        if (_objectSelection.GlobalSelectableObjects.Contains(_self) == false && Vector3.Magnitude(_self.transform.position - _user.transform.position) > _objectSelection.DirectDistance)
            _objectSelection.GlobalSelectableObjects.Add(_self);
        
        Vector3 microPosition = _self.transform.position - _microSelectionOrigin.transform.position;
        MicroAngle = Vector3.Angle(microPosition, _microSelectionOrigin.transform.forward);
        
        Vector3 macroPosition = _self.transform.position - _user.transform.position;
        MacroAngle = Vector3.Angle(macroPosition, _user.transform.forward);

        if (_objectSelection.ActiveObject == _self)
            _stateCounter++;
        else if (_objectSelection.ActiveObject != _self)
            _stateCounter = 0;

        if (_stateCounter == 1)
            Invoke("OnSelectBegin", 0);        
    }

    public void OnSelectBegin()
    {
        
    }

    public void OnSelectEnd()
    {
        
    }

    public void OnGrabBegin()
    {
        transform.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
    }

    public void OnGrabStay()
    {
        transform.position = Vector3.Lerp(transform.position, _lerpTarget.transform.position, _indirectManipulation.LerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.localRotation, _lerpTarget.transform.localRotation, _indirectManipulation.LerpSpeed);
    }
    
    public void OnGrabEnd()
    {

    }
    
    public void DrawLineRenderer()
    {
        _lineRenderer = _self.transform.GetComponent<LineRenderer>();
        if (_lineRenderer == null)
            _lineRenderer = _self.AddComponent<LineRenderer>();
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.SetWidth(0.0005f, 0.0005f);
        _lineRenderer.SetVertexCount(2);
        _lineRenderer.SetPosition(0, _microSelectionOrigin.transform.position);
        _lineRenderer.SetPosition(1, _self.transform.position);
    }

    public void RemoveLineRenderer()
    {
        if (_lineRenderer != null)
            _lineRenderer.SetVertexCount(0);
    }
}
