using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnShellTrigger : MonoBehaviour {

    public UnityEvent _onShellOpen;
    public UnityEvent _onShellClose;

    public void OnShellOpen()
    {
        _onShellOpen.Invoke();
    }

    public void OnShellClose()
    {
        _onShellClose.Invoke();
    }
}
