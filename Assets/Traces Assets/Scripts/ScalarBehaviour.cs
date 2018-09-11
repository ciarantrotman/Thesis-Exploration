using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalarBehaviour : MonoBehaviour
{
    private GameObject _scaleTarget;
    private GameObject _ctrlAct;
    private float _initDist;
    private Vector3 _initScale;
    public void ScalingActionBegin()
    {
        _scaleTarget = GameObject.Find("HMD Camera").GetComponent<ObjectSelection>().LastActiveObject;
        _ctrlAct = GameObject.Find("CtrlAct");
        _initDist = Vector3.Magnitude(transform.position - _ctrlAct.transform.position);
        _initScale = _scaleTarget.transform.localScale;
    }
    
    public void ScalingActionStay()
    {
        var scaleRat = Vector3.Magnitude(transform.position - _ctrlAct.transform.position) / _initDist;
        _scaleTarget.transform.localScale = _initScale * scaleRat;
    }
}
