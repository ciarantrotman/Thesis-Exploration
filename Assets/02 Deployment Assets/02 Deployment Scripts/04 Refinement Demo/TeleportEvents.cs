using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportEvents : MonoBehaviour {
    public UnityEvent _onTeleport;
    public void Teleport()
    {
        _onTeleport.Invoke();
    }
}
