using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VoiceGesture : MonoBehaviour
{
    public UnityEvent AssistantActivate;
    public UnityEvent AssistantDeactivate;
    
    
    private void OnCollisionEnter(Collision hmdCollision)
    {
        if (hmdCollision.gameObject.name == "IndexTip")
            AssistantActivate.Invoke();
    }

    private void OnCollisionExit(Collision hmdCollision)
    {
        if (hmdCollision.gameObject.name == "IndexTip")
            AssistantDeactivate.Invoke();
    }
}
