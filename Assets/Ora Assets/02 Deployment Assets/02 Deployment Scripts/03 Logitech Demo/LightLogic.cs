using System.Collections;
using Leap.Unity.Attributes;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Leap.Unity.Interaction;

public class LightLogic : MonoBehaviour {

    public GameObject SliderObject;
    public Light LightOne;
    public Light LightTwo;
    public float SliderMultiplier = 2f;

    private InteractionSlider HorizontalSlider;
    private float SliderValue;

    public Color White = new Color32(255, 244, 229, 255);
    public Color Red = new Color32(236, 91, 114, 255);
    public Color Blue = new Color32(61, 121, 175, 255);

    void Start () {
        LightOne = LightOne.GetComponent<Light>();
        LightTwo = LightTwo.GetComponent<Light>();

        LightOne.intensity = SliderValue;
        LightTwo.intensity = SliderValue;
    }

    void Update () {
        HorizontalSlider = SliderObject.GetComponent<InteractionSlider>();
        SliderValue = HorizontalSlider.HorizontalSliderPercent;
        SliderValue = SliderValue * SliderMultiplier;

        LightOne.intensity = SliderValue;
        LightTwo.intensity = SliderValue;
    }

    public void WhiteLight()
    {
        /*
        Color L1Current = LightOne.color;
        Color L2Current = LightTwo.color;
        LightOne.color = Color.Lerp(L1Current, White, .3F);
        LightTwo.color = Color.Lerp(L2Currentr, White, .3F);
        */

        LightOne.color = White;
        LightTwo.color = White;
    }

    public void BlueLight()
    {
        LightOne.color = Blue;
        LightTwo.color = Blue;
    }

    public void RedLight()
    {
        LightOne.color = Red;
        LightTwo.color = Red;
    }

    public void LightOn()
    {

    }

    public void LightOff()
    {

    }
}
