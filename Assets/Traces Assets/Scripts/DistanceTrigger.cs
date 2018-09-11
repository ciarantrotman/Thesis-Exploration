using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DistanceTrigger : MonoBehaviour
{
	public Transform TriggerTarget;
	public float TriggerDistance;
	public UnityEvent WithinRange;
	public UnityEvent OutOfRange;
	private int counter1 = 0;
	private int counter2 = 0;

	private void Update ()
	{
		var triggerDistance = Vector3.Magnitude(TriggerTarget.transform.position - transform.position);
		if (triggerDistance <= TriggerDistance && counter1 == 0)
		{
			counter1++;
			counter2 = 0;
			WithinRange.Invoke();
		}
		if (triggerDistance > TriggerDistance && counter2 == 0)
		{
			counter1 = 0;
			counter2++;
			OutOfRange.Invoke();
		}
	}
}
