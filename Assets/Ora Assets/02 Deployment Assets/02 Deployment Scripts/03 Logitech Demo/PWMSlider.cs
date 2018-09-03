using System.Collections;
using UnityEngine;
using Uniduino;
using Leap.Unity.Interaction;

public class PWMSlider : MonoBehaviour
{
    private Arduino arduino;

    public GameObject SliderObject;
    private InteractionSlider HorizontalSlider;
    private float SliderValue;
    private int SliderValueInt;

    private float SmoothVelocity = 1f;
    private int PWMPin = 3;
    private float VoltageFloat = 0f;
    private int VoltageInt = 0;

    public int HapticIntensity = 175;
    public int HapticFrequency = 5;

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);

        SliderValueInt = (int)SliderValue;
        VoltageInt = (int)VoltageFloat;
    }

    void ConfigurePins()
    {
        arduino.pinMode(PWMPin, PinMode.PWM);
    }

    void Update()
    {
        HorizontalSlider = SliderObject.GetComponent<InteractionSlider>();
        SliderValue = HorizontalSlider.HorizontalSliderPercent;
        SliderValue = SliderValue * 100;

        //arduino.analogWrite(PWMPin, VoltageInt);
    }

    public void OnSlide()
    {
        if (SliderValueInt % HapticFrequency == 0 && SliderValueInt != 100 && SliderValueInt != 0)
        {
            /*while (VoltageFloat < (HapticIntensity - 1))
            {
                VoltageFloat = (Mathf.SmoothDamp(VoltageFloat, HapticIntensity, ref SmoothVelocity, .01f));
            }*/
            arduino.analogWrite(PWMPin, HapticIntensity);
            Debug.Log("What");
        }

        else
        {
            arduino.analogWrite(PWMPin, 0);
        }
    }
}