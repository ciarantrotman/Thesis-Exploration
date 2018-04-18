using System.Collections;
using Leap.Unity.Attributes;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Leap.Unity.Interaction;
using UnityEngine.Events;

public class BrowserSlider : MonoBehaviour
{
    public UnityEvent _left;
    public UnityEvent _center;
    public UnityEvent _right;
    public GameObject SliderObject;
    private float SliderMultiplier = 100.0f;
    private InteractionSlider HorizontalSlider;
    private float _slidervalue;

    void Update()
    {
        HorizontalSlider = SliderObject.GetComponent<InteractionSlider>();
        _slidervalue = HorizontalSlider.HorizontalSliderPercent;
        _slidervalue = _slidervalue * SliderMultiplier;
        if (_slidervalue < 33)
        {
            _left.Invoke();
        }
        if (_slidervalue > 33 && _slidervalue < 65)
        {
            _center.Invoke();
        }
        if (_slidervalue > 66)
        {
            _right.Invoke();
        }
    }
}
