using System.Collections;
using UnityEngine;
using Uniduino;

public class VibrateOnClickTEST : MonoBehaviour
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

    public void OnClick()
    {
        StartCoroutine(ConstantVibrateStart());
    }

    private IEnumerator ConstantVibrateStart()
    {
        if (clicked == false)
        {
            arduino.digitalWrite(13, Arduino.HIGH);
            yield return new WaitForSeconds(.25F);
            arduino.digitalWrite(13, Arduino.LOW);
            clicked = true;
        }
    }

    public void OnUnclick()
    {
        StartCoroutine(ConstantVibrateStop());
    }

    private IEnumerator ConstantVibrateStop()
    {
        if (clicked == true)
        {
            clicked = false;
            yield return new WaitForSeconds(.25F);
        }
    }
}