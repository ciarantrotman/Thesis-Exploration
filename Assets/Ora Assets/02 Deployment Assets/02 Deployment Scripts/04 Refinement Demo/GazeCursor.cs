using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GazeCursor : MonoBehaviour
{
    [Header("Gaze Cursor States")]
    public GameObject _defaultCursor;
    public GameObject _grabCursor;
    public GameObject _selectCursor;
    [Space(10)]
    [Header("Gaze Cursor Events")]
    public UnityEvent _onProgramGrab;
    public UnityEvent _onProgramRelease;
    public void OnGrab()
    {
        _onProgramGrab.Invoke();
    }
    public void OnRelease()
    {
        _onProgramRelease.Invoke();
    }
    [Space(10)]
    [Header("Selection Events")]
    public UnityEvent _onSelect;
    public UnityEvent _onDeselect;
    public void OnSelect()
    {
        _onSelect.Invoke();
    }
    public void OnDeselect()
    {
        _onDeselect.Invoke();
    }

    private GameObject _cursor;
    private GameObject _user;
    private float _cursorDistance;
    private float _scaleFactor;
    void Start()
    {
        _cursor = gameObject;
        _user = GameObject.Find("HMD Camera");
    }
    public void Update()
    {
        _cursorDistance = Vector3.Distance(_cursor.transform.position, _user.transform.position);
        _scaleFactor = _cursorDistance / 2;
        transform.localScale.Set(_scaleFactor, _scaleFactor, _scaleFactor);
    }
}
