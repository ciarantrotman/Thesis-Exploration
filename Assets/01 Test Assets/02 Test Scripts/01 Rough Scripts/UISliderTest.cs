using System.Collections;
using Leap.Unity.Attributes;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Leap.Unity.Interaction;

public class UISliderTest : MonoBehaviour {

    SkinnedMeshRenderer skinnedMeshRenderer;

    public GameObject SliderObject;
    public int BlendshapeNumber = 0;
    private float BlendVelocity = 0.0f;

    private InteractionSlider HorizontalSlider;
    private float SliderValue;

    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    void Update()
    {
        HorizontalSlider = SliderObject.GetComponent<InteractionSlider>();
        SliderValue = HorizontalSlider.HorizontalSliderPercent;
        SliderValue = SliderValue * 100;
    }

    public void OnSlider()
    {
        skinnedMeshRenderer.SetBlendShapeWeight(BlendshapeNumber, SliderValue);
    }
}