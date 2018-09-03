using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class animTrigger : MonoBehaviour {
    public UnityEvent _startAnimations;
    private void Awake()
    {
        _startAnimations.Invoke();
    }
}
