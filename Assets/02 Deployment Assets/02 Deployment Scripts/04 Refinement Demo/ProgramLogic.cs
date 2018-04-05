using UnityEngine;
using UnityEngine.Events;

public class ProgramLogic : MonoBehaviour
{
    void Start()
    {
        _program = gameObject;
        _grabProxy = GameObject.Find("Psuedo Direct Grab Target").transform;
        _userHand = GameObject.Find("Direct Grab Reference Point").transform;
        _user = GameObject.Find("HMD Camera");
    }
    void Update()
    {
        _programDistance = Vector3.Distance(_program.transform.position, _user.transform.position);
        _proxyDistance = Vector3.Distance(_user.transform.position, _userHand.transform.position);
        if (_programDistance < _directDistance)
        {
            _onBecomingDirect.Invoke();
        }
        if (_programDistance > _directDistance)
        {
            _onBecomingIndirect.Invoke();
        }
        if (_grabState == true)
        {
            float distCovered = (Time.time - startTime) * _lerpSpeed;
            float fracJourney = distCovered / journeyLength;
            _program.transform.position = Vector3.Lerp(_program.transform.position, _grabProxy.position, fracJourney);
            _program.transform.rotation = Quaternion.Lerp(_program.transform.rotation, _grabProxy.rotation, fracJourney);
            _programXPos = Mathf.SmoothDamp(_program.transform.position.x, _grabProxy.position.x, ref _velocity, 0);
            _programYPos = Mathf.SmoothDamp(_program.transform.position.y, _grabProxy.position.y, ref _velocity, 0);
            _programZPos = Mathf.SmoothDamp(_program.transform.position.z, _grabProxy.position.z, ref _velocity, 0);
            _program.transform.position.Set(_programXPos, _programYPos, _programZPos);
        }
    }
    #region Direct Transitions          | 00
    private GameObject _program;
    private GameObject _user;
    private float _programDistance;
    [Header("Direct and Indirect Transition Events")]
    [Space(5)]
    public float _directDistance = .55f;
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
    #region Grabbing and Releasing      | 10
    [Space(10)]
    [Header("Grab and Release Events")]
    private Transform _grabProxy;
    private Transform _userHand;
    public float _lerpSpeed = 1.0F;
    private bool _grabState;
    private float startTime;
    private float journeyLength;

    private float _initialProxyDistance;
    private float _initialProgramDistance;
    private float _proxyDistance;
    private float _programXPos;
    private float _programYPos;
    private float _programZPos;
    private float _velocity = 0.0F;

    [Space(5)]
    public UnityEvent _onProgramGrab;
    public UnityEvent _onProgramRelease;
    public void OnGrab()
    {
        _onProgramGrab.Invoke();
        journeyLength = Vector3.Distance(transform.position, _grabProxy.position);
        startTime = 0;
        _initialProxyDistance = Vector3.Distance(_user.transform.position, _userHand.position);
        _initialProgramDistance = Vector3.Distance(_user.transform.position, _program.transform.position);
    }
    public void OnRelease()
    {
        _onProgramRelease.Invoke();
    }
    public void Grab()
    {
        _grabState = true;
    }
    public void Release()
    {
        _grabState = false;
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
