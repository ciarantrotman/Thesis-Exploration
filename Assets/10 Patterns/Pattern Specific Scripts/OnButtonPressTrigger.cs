using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OnButtonPressTrigger : MonoBehaviour
{
    public UnityEvent OnButtonPress;
    public UnityEvent OnButtonRelease;

    void Update()
    {
        if (GameObject.Find("Manual Input Controller").GetComponent<IndirectGrab>()._rightButtonPress == true)
        {
            OnButtonPress.Invoke();
        }
        else if (GameObject.Find("Manual Input Controller").GetComponent<IndirectGrab>()._rightButtonPress == false)
        {
            OnButtonRelease.Invoke();
        }
    }
}