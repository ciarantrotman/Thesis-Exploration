using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PocketLogic : MonoBehaviour {
    [Header("Pocket Gaze Events")]
    [Space(5)]
    public UnityEvent _onGazeBegin;
    public UnityEvent _onGazeEnd;
    public void OnGazeBegin()
    {
        _onGazeBegin.Invoke();
    }
    public void OnGazeEnd()
    {
        _onGazeEnd.Invoke();
    }
}
