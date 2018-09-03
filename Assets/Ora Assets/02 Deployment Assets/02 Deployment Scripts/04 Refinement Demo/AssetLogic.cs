using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AssetLogic : MonoBehaviour
{
    /*
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
    private bool _grabState;
    private float _proxyDistance;
    [Space(5)]
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
    public void Grab()
    {
        _grabState = true;
    }
    public void Release()
    {
        _grabState = false;
    }
    #endregion
    #region Collidion Events
    [Header("Collision Events")]
    [Space(5)]
    public UnityEvent _onColliderEnter;
    public UnityEvent _onColliderExit;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "Diorama")
        {
            _onColliderEnter.Invoke();
        }
    }
    public void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "Diorama")
        {
            _onColliderExit.Invoke();
        }
    }
    #endregion*/
}
