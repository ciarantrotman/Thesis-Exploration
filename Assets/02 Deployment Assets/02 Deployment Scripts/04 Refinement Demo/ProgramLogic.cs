using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgramLogic : MonoBehaviour
{
    #region Direct and Indirect Transitions
    private GameObject _program;
    private GameObject _user;
    private float _programDistance;
    [Header("Direct and Indirect Transition Events")]
    [Space(5)]
    public float _directDistance = .55f;
    public UnityEvent _onBecomingDirect;
    public UnityEvent _onBecomingIndirect;
    void Start()
    {
        _program = gameObject;
        _user = GameObject.Find("HMD Camera");
    }
    public void Update()
    {
        _programDistance = Vector3.Distance(_program.transform.position, _user.transform.position);
        if (_programDistance < _directDistance)
        {
            _onBecomingDirect.Invoke();
        }
        if (_programDistance > _directDistance)
        {
            _onBecomingIndirect.Invoke();
        }
    }
    public void OnBecomingDirect()
    {
        _onBecomingDirect.Invoke();
    }
    public void OnBecomingIndirect()
    {
        _onBecomingIndirect.Invoke();
    }
    #endregion
    #region Launching and Closing
    [Space(10)]
    [Header("Program Launch and Close Events")]
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
    [Space(10)]
    [Header("Shell Launch and Close Events")]
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
    [Space(10)]
    [Header("Hover Events")]
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
    #region Summoning
    [Space(10)]
    [Header("Summon Events")]
    public UnityEvent _onSummon;
    public UnityEvent _onUnsummon;
    public void OnSummon()
    {
        _onSummon.Invoke();
    }
    public void OnUnsummon()
    {
        _onUnsummon.Invoke();
    }
    #endregion
}
