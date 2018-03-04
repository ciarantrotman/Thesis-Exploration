using System.Collections;
using UnityEngine;
using Uniduino;

public class PWMTest : MonoBehaviour {

    public Arduino arduino;

    public int OutputPin = 3;
    public int Voltage = 200;

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }

    void ConfigurePins()
    {
        arduino.pinMode(OutputPin, PinMode.PWM);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("UP " + OutputPin);
            arduino.analogWrite(OutputPin, (int)200);

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("DOWN " + OutputPin);
            arduino.analogWrite(OutputPin, (int)0);
        }
    }
}
