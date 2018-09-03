using System.Collections;
using UnityEngine;
using Uniduino;

public class PushButtonSetChildActive : MonoBehaviour
{

    public Arduino arduino;

    public int pin = 2;
    public int pinValue;
    public GameObject ChildObject;

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }

    void ConfigurePins()
    {
        arduino.pinMode(pin, PinMode.INPUT);
        arduino.reportDigital((byte)(pin / 8), 1);
    }

    void Update()
    {
        pinValue = arduino.digitalRead(pin);

        if (pinValue == 1)
        {
            ChildObject.SetActive(true);
            Debug.Log("Button Pressed");
        }

        else if (pinValue == 0)
        {
            Debug.Log("Button Not Pressed");
        }

    }

}