using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class FingerHaptics : MonoBehaviour
{
    private Arduino arduino;

    [Header("Arduino Settings")]
    public int MotorPin = 3;

    [Header("Haptic Effects")]
    public float SmoothInSpeed = 1.0F;
    public float SmoothOutSpeed = 1.0F;
    [Space(5)]
    public float LowerTarget = 0.0F;
    public float UpperTarget = 150.0f;
    [Space(5)]
    public float HapticTriggerDistance = 0.02f;

    private float SmoothVelocity = 0.0f;
    private float VoltageFloat;
    private int VoltageInt;

    private bool Trigger = false;

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }

    void ConfigurePins()
    {
        arduino.pinMode(MotorPin, PinMode.PWM);
    }

    void FixedUpdate()
    {
        Ray FingerRaycast = new Ray(transform.position, transform.forward);
        RaycastHit HitPoint;

        if (Physics.Raycast(FingerRaycast, out HitPoint, HapticTriggerDistance) && HitPoint.transform.tag == "HapticObject")
        {
            Trigger = true;
            StartCoroutine(SmoothVibrateStart());
        }

        else
        {
            Trigger = false;
            StartCoroutine(SmoothVibrateStop());
        }
    }

    private IEnumerator SmoothVibrateStart()
    {
        while (Trigger == true && VoltageFloat < (UpperTarget - 3))
        {
            VoltageInt = (Mathf.RoundToInt(VoltageFloat));
            VoltageFloat = (Mathf.SmoothDamp(VoltageFloat, UpperTarget, ref SmoothVelocity, SmoothInSpeed));
            arduino.analogWrite(MotorPin, VoltageInt);
            yield return null;
        }
    }

    private IEnumerator SmoothVibrateStop()
    {
        while (VoltageFloat > (LowerTarget + 1))
        {
            VoltageInt = (Mathf.RoundToInt(VoltageFloat));
            VoltageFloat = (Mathf.SmoothDamp(VoltageFloat, LowerTarget, ref SmoothVelocity, SmoothOutSpeed));
            arduino.analogWrite(MotorPin, VoltageInt);
            yield return null;
        }
    }
}
