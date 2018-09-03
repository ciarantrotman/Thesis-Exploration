using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotificationLogic : MonoBehaviour {
    public UnityEvent _onNotification;
    public void Notification()
    {
        _onNotification.Invoke();
    }
}
