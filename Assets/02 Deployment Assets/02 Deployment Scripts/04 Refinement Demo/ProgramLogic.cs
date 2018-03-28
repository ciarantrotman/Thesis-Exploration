using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgramLogic : MonoBehaviour {
    #region Launching and Closing
    public UnityEvent _onProgramLaunch;
    public UnityEvent _onProgramClose;
    public void OnLaunch()
    {
        _onProgramLaunch.Invoke();
    }
    public void OnClose()
    {
        _onProgramClose.Invoke();
    }
    #endregion
    #region Shell
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
    #endregion
    #region Indirect Hover
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
    #endregion
    #region Direct and Indirect Transitions
    public UnityEvent _onBecomingDirect;
    public UnityEvent _onBecomingIndirect;
    public void OnBecomingDirect()
    {
        _onBecomingDirect.Invoke();
    }
    public void OnBecomingIndirect()
    {
        _onBecomingIndirect.Invoke();
    }
    #endregion
}
