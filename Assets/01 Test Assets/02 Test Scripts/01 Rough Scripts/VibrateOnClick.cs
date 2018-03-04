using System.Collections;
using UnityEngine;
using Uniduino;

public class VibrateOnClick : MonoBehaviour
{

    public Arduino arduino;

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }

    void ConfigurePins()
    {
        arduino.pinMode(13, PinMode.OUTPUT);
    }

    // CONSTANT VIBRATION

    public void ConstantVibrationStart()
    {
        StartCoroutine(ConstantVibrateStart());
    }

    private IEnumerator ConstantVibrateStart()
    {
        arduino.digitalWrite(13, Arduino.HIGH);
        yield return new WaitForSeconds(1);
    }

    public void ConstantVibrationStop()
    {
        StartCoroutine(ConstantVibrateStop());
    }

    private IEnumerator ConstantVibrateStop()
    {
        arduino.digitalWrite(13, Arduino.LOW);
        yield return new WaitForSeconds(1);
    }

    // PULSE VIBRATION

    public void PulseVibrate()
    {
        StartCoroutine(PulseVibration());
    }

    private IEnumerator PulseVibration()
    {
        arduino.digitalWrite(13, Arduino.HIGH);
        yield return new WaitForSeconds(.25F);
        arduino.digitalWrite(13, Arduino.LOW);
        yield return new WaitForSeconds(.25F);
    }

    // SHORT VIBRATION

    public void ShortVibrate()
    {
        StartCoroutine(ShortVibration());
    }

    private IEnumerator ShortVibration()
    {
        arduino.digitalWrite(13, Arduino.HIGH);
        yield return new WaitForSeconds(.1F);
        arduino.digitalWrite(13, Arduino.LOW);
        StopCoroutine(ShortVibration());
    }

}