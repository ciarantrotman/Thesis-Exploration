using System.Collections;
using UnityEngine;
using Uniduino;

public class HapticCoroutines : MonoBehaviour
{

    public Arduino arduino;
    private bool clicked = false;

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }

    void ConfigurePins()
    {
        arduino.pinMode(13, PinMode.OUTPUT);
    }

    //

    public void ShortVibrate()
    {
        StartCoroutine(ShortVibration());
    }

    private IEnumerator ShortVibration()
    {
        if (clicked == false)
        {
            arduino.digitalWrite(13, Arduino.HIGH);
            yield return new WaitForSeconds(.25F);
            arduino.digitalWrite(13, Arduino.LOW);
            clicked = true;
        }
    }

    //

    public void HapticPulse()
    {
        StartCoroutine(HapticPulses());
    }

    private IEnumerator HapticPulses()
    {
        if (clicked == false)
        {
            arduino.digitalWrite(13, Arduino.HIGH);
            yield return new WaitForSeconds(.1F);
            arduino.digitalWrite(13, Arduino.LOW);
            yield return new WaitForSeconds(.1F);
            arduino.digitalWrite(13, Arduino.HIGH);
            yield return new WaitForSeconds(.1F);
            arduino.digitalWrite(13, Arduino.LOW);
            yield return new WaitForSeconds(.1F);
            clicked = true;
        }
    }

    //

    public void ResetHaptics()
    {
        if (clicked == true)
        {
            clicked = false;
        }
    }
}