using System.Collections;
using Leap.Unity.Attributes;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Leap.Unity.Interaction;

public class LightSlider : MonoBehaviour
{

    SkinnedMeshRenderer skinnedMeshRenderer;

    public GameObject SliderObject;
    public Light LightObject;
    public float SliderMultiplier = 100.0f;

    private InteractionSlider HorizontalSlider;
    private float SliderValue;

    void Start()
    {
        LightObject = LightObject.GetComponent<Light>();
        LightObject.intensity = SliderValue;
    }

    void Update()
    {
        HorizontalSlider = SliderObject.GetComponent<InteractionSlider>();
        SliderValue = HorizontalSlider.HorizontalSliderPercent;
        SliderValue = SliderValue * SliderMultiplier;
    }

    public void OnSlider()
    {
        LightObject.intensity = SliderValue;
    }
}