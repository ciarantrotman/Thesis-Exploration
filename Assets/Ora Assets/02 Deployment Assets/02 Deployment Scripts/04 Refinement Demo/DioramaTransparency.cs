using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DioramaTransparency : MonoBehaviour
{
    private GameObject _rightHand;
    private GameObject _leftHand;
    private float _rightDistance;
    private float _leftDistance;
    private float _distancePercentage;
    private float _blendshapeValue;
    private int _blendshapeIndex = 0;
    [Header("Distance Based Blendshape Settings")]
    [Space(5)]
    public bool _leftHandTrigger = true;
    public bool _rightHandTrigger = true;
    public float _distanceThreshold = .2f;
    void Start()
    {
        _rightHand = GameObject.Find("Right Hand Proxy");
        _leftHand = GameObject.Find("Left Hand Proxy");
    }
    void Update()
    {
        /*
        _rightDistance = _rightHandTrigger ? Vector3.Distance(transform.position, _rightHand.transform.position) : 100;
        _leftDistance = _leftHandTrigger ? Vector3.Distance(transform.position, _leftHand.transform.position) : 100;
        if (_rightDistance < _distanceThreshold || _leftDistance < _distanceThreshold)
        {
            _blendshapeValue = (_rightDistance / _distanceThreshold) * 100;
            gameObject.GetComponent<Renderer>().material.color.a = _blendshapeValue;
        }
        if (_leftDistance < _distanceThreshold)
        {
            _blendshapeValue = (_leftDistance / _distanceThreshold) * 100;
        }
        if (_rightDistance >= _distanceThreshold && _leftDistance >= _distanceThreshold)
        {
            gameObject.GetComponent<Renderer>().material.color.a = 0;
        }
        */
    }
}

