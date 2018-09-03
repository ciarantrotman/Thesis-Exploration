using UnityEngine;
using System.Collections;
using Uniduino;

public class ActivateHardwareButton : MonoBehaviour
{
    private Arduino arduino;
    public int InputPin = 2;
    public int InputPinValue = 1;
    private int PressState = 1;

    public GameObject TargetObject;

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }

    void ConfigurePins()
    {
        arduino.pinMode(InputPin, PinMode.INPUT);
        arduino.reportDigital((byte)(InputPin / 8), 1);
    }

    void Update()
    {
        InputPinValue = arduino.digitalRead(InputPin);

        if (InputPinValue == 0)
        {
            PressState++;
        }

        /*if (PressState / 2)
        {
            TargetObject.active = true;
        }

        else
        {
            TargetObject.active = false;
        }*/
    }
}