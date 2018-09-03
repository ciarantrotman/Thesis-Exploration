using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OnHoverTrigger : MonoBehaviour {

    public UnityEvent _onHoverStart;
    public UnityEvent _onHoverEnd;

    public void OnHoverStart()
    {
        _onHoverStart.Invoke();
    }

    public void OnHoverEnd()
    {
        _onHoverEnd.Invoke();
    }
}
