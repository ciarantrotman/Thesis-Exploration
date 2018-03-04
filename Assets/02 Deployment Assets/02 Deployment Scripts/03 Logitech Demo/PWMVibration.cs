using System.Collections;
using UnityEngine;
using Uniduino;
using Leap.Unity.Interaction;

public class PWMVibration : MonoBehaviour
{

    private Arduino arduino;
    public float SmoothDuration = 1.0F;
    public int PWMPin = 3;

    private float SmoothVelocity = 0.0f;
    private float VoltageFloat;
    private int VoltageInt;

    public float LowerTarget = 0.0F;
    public float UpperTarget = 200.0f;

    private bool Trigger = false;

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }

    void ConfigurePins()
    {
        arduino.pinMode(PWMPin, PinMode.PWM);
    }

    public void OnPressSmoothVibrate()
    {
        Trigger = true;
        StartCoroutine(SmoothVibrateStart());
    }

    private IEnumerator SmoothVibrateStart()
    {
        while (Trigger == true && VoltageFloat < (UpperTarget - 1))
        {
            VoltageInt = (Mathf.RoundToInt(VoltageFloat));
            VoltageFloat = (Mathf.SmoothDamp(VoltageFloat, UpperTarget, ref SmoothVelocity, SmoothDuration));
            arduino.analogWrite(PWMPin, VoltageInt);
            yield return null;
        }    
    }

    public void OnUnpressSmoothVibration()
    {
        Trigger = false;
        StartCoroutine(SmoothVibrateStop());
    }

    private IEnumerator SmoothVibrateStop()
    {
        while (VoltageFloat > (LowerTarget + 1))
        {
            VoltageInt = (Mathf.RoundToInt(VoltageFloat));
            VoltageFloat = (Mathf.SmoothDamp(VoltageFloat, LowerTarget, ref SmoothVelocity, SmoothDuration));
            arduino.analogWrite(PWMPin, VoltageInt);
            yield return null;
        }
    }

    public void OnPressClick()
    {
        Trigger = true;
        StartCoroutine(PressClick());
    }

    private IEnumerator PressClick()
    {
        if (Trigger == true)
        {
            arduino.analogWrite(PWMPin, 150);
            yield return new WaitForSeconds(.25f);
            arduino.analogWrite(PWMPin, 0);
            Trigger = false;
        }
    }
}