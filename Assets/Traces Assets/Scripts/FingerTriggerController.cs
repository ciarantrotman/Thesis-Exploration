using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FingerTriggerController : MonoBehaviour {

	public UnityEvent IndexFinger;
	public UnityEvent MiddleFinger;
	public UnityEvent RingFinger;
	public UnityEvent PinkyFinger;
    
	private void OnCollisionEnter(Collision thumbCollision)
	{
		if (thumbCollision.gameObject.name == "IndexTip")
			IndexFinger.Invoke();
		if (thumbCollision.gameObject.name == "MiddleTip")
			MiddleFinger.Invoke();
		if (thumbCollision.gameObject.name == "RingTip")
			RingFinger.Invoke();
		if (thumbCollision.gameObject.name == "PinkyTip")
			PinkyFinger.Invoke();
	}
}
