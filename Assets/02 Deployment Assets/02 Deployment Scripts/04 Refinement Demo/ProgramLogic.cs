using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgramLogic : MonoBehaviour
{
    #region Direct Transitions          | 00
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
    #region Grabing and Releasing       | 10
    [Space(10)]
    [Header("Grab and Release Events")]
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
    #endregion
    #region Launching and Closing       | 20
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
    #region Selection                   | 30
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
    #endregion
    #region Shell                       | 40
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
    #region Indirect Hover              | 50
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
    #region Summoning                   | 60
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
