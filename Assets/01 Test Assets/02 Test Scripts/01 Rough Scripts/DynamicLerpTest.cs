using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLerpTest : MonoBehaviour {

    private float StartTime;
    private float JourneyLength;
    private float CurrentLerpTime = 0.0f;
    private bool LerpState = false;

    [Header("Lerp Settings")]
    [Space(5)]
    public float LerpTime = 1.0F;
    public GameObject TargetLocation;

    void Start ()
    {

    }

	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            if (LerpState == false)
            {
                LerpState = true;
            }

            else
            {
                LerpState = false;
                CurrentLerpTime = 0;
            }
        }

        if (LerpState == true)
        {
            CurrentLerpTime += Time.deltaTime;

            if (CurrentLerpTime >= LerpTime)
            {
                CurrentLerpTime = 0;
            }

            float JourneyPercentage = CurrentLerpTime / LerpTime;

            this.transform.position = Vector3.Lerp(this.transform.position, TargetLocation.transform.position, JourneyPercentage);
        }
	}
}
