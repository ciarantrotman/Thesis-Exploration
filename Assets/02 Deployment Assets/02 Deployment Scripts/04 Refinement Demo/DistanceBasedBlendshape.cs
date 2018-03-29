using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceBasedBlendshape : MonoBehaviour
{
    private GameObject _blendshapeObject;
    private GameObject _userHand;
    private float _distance;
    private float _distancePercentage;
    private float _blendshapeValue;
    private int _blendshapeIndex = 0;
    SkinnedMeshRenderer skinnedMeshRenderer;
    [Header("Distance Based Blendshape Settings")]
    [Space(5)]
    public float _distanceThreshold = .2f;
    void Start ()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        _blendshapeObject = gameObject;
        _userHand = GameObject.Find("Right Hand Proxy");
    }
	void Update ()
    {
        _distance = Vector3.Distance(_blendshapeObject.transform.position, _userHand.transform.position);
        if (_distance < _distanceThreshold)
        {
            _blendshapeValue = (_distance/_distanceThreshold)*100;
            skinnedMeshRenderer.SetBlendShapeWeight(_blendshapeIndex, _blendshapeValue);
        }
    }
}
