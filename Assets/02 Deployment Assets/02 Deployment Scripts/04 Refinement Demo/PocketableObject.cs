using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PocketableObject : MonoBehaviour
{
    [Header("Pocketing Events")]
    public UnityEvent _onPocket;
    public UnityEvent _onUnpocket;
    private GameObject _originalParent;
    private GameObject _program;
    void Start()
    {
        _program = transform.gameObject;
        _originalParent = _program.transform.parent.transform.gameObject;
    }
    void OnTriggerEnter(Collider _collider)
    {
        if (_collider.GetComponent<Collider>().name == "Pocket | Master")
        {
            _onPocket.Invoke();
        }
    }
    void OnTriggerExit(Collider _collider)
    {
        if (_collider.GetComponent<Collider>().name == "Pocket | Master")
        {
            _onUnpocket.Invoke();
        }
    }
}
