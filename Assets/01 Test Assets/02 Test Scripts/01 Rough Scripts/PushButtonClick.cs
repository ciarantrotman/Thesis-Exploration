using System.Collections;
using UnityEngine;
using Uniduino;

public class PushButtonClick : MonoBehaviour
{

    public Arduino arduino;

    public int pin = 2;
    public int pinValue;
    public int testLed = 13;

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }

    void ConfigurePins()
    {
        arduino.pinMode(pin, PinMode.INPUT);
        arduino.reportDigital((byte)(pin / 8), 1);
        arduino.pinMode(testLed, PinMode.OUTPUT);
    }

    void Update()
    {
        pinValue = arduino.digitalRead(pin);
        arduino.digitalWrite(testLed, pinValue);
    }

}