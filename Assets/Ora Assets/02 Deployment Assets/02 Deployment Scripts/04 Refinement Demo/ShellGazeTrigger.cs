using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShellGazeTrigger : MonoBehaviour {

    [Header("Gaze Trigger Settings:")]
    [Space(5)]
    private int ReachDistance = 100;
    private GameObject _mainCamera;

    [Header("Shell Launch Methods:")]
    [Space(5)]
    public UnityEvent OnGazeBegin;
    public UnityEvent OnGazeEnd;

    void Start()
    {
        _mainCamera = GameObject.Find("HMD Camera");
    }

    void Update()
    {
        Ray GazeRay = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        RaycastHit GazeFocus;

        if (Physics.Raycast(GazeRay, out GazeFocus, ReachDistance) && GazeFocus.transform.tag == "ProprioShell")
        {
            OnGazeBegin.Invoke();
        }
        else
        {
            OnGazeEnd.Invoke();
        }
    }
}
