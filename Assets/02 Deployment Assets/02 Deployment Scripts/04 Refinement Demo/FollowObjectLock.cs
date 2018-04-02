using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectLock : MonoBehaviour {
    [Header("Locking Settings")]
    [Space(5)]
    public GameObject FollowedObject;
    [Space(5)]
    [SerializeField] private bool _lockXPosition;
    [SerializeField] private bool _lockYPosition;
    [SerializeField] private bool _lockZPosition;
    [Space(10)]
    [SerializeField] private bool _lockXRotation;
    [SerializeField] private bool _lockYRotation;
    [SerializeField] private bool _lockZRotation;

    private Vector3 _defaultPosition;
    private Vector3 _defaultRotation;

    private void Awake()
    {
        _defaultPosition = transform.localPosition;
        _defaultRotation = transform.eulerAngles;
    }


    void Update()
    {
        var position = transform.localPosition;
        transform.localPosition = new Vector3(
            _lockXPosition ? _defaultPosition.x : FollowedObject.transform.position.x,
            _lockYPosition ? _defaultPosition.y : FollowedObject.transform.position.y,
            _lockZPosition ? _defaultPosition.z : FollowedObject.transform.position.z);
        var rotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(
            _lockXRotation ? _defaultRotation.x : FollowedObject.transform.rotation.x,
            _lockYRotation ? _defaultRotation.y : FollowedObject.transform.rotation.y,
            _lockZRotation ? _defaultRotation.z : FollowedObject.transform.rotation.z);
    }
}
