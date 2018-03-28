using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExocentricLinerenderer : MonoBehaviour {

    private LineRenderer _exocentricLinerenderer;
    private GameObject _startPoint;
    public GameObject _endPoint;

    void Start ()
    {
        _startPoint = transform.gameObject;
        _exocentricLinerenderer = GetComponent<LineRenderer>();
    }
	
	void Update ()
    {
        _exocentricLinerenderer.SetPosition(0, _startPoint.transform.position);
        _exocentricLinerenderer.SetPosition(1, _endPoint.transform.position);
    }
}
