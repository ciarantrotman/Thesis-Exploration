using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabEventTrigger : MonoBehaviour
{
    public UnityEvent OnGrab;
    
    private void OnCollisionEnter(Collision indexFinger)
    {
        if (indexFinger.gameObject.name == "IndexDistalJoint")
            OnGrab.Invoke();
    }
}
