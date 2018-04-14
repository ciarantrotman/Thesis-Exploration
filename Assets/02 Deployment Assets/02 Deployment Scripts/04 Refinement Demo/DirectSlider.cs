using System.Collections;
using Leap.Unity.Attributes;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Leap.Unity.Interaction;
using UnityEngine.Events;

public class DirectSlider : MonoBehaviour
{
    public UnityEvent _onSummon;
    public UnityEvent _onUnsummon;
    public GameObject SliderObject;
    public float SliderMultiplier = 100.0f;
    private InteractionSlider HorizontalSlider;
    private float _slidervalue;

    void Update()
    {
        HorizontalSlider = SliderObject.GetComponent<InteractionSlider>();
        _slidervalue = HorizontalSlider.HorizontalSliderPercent;
        _slidervalue = _slidervalue * SliderMultiplier;
        //Debug.Log(_slidervalue);
        if (_slidervalue > 90)
        {
            _onSummon.Invoke();
        }
        if (_slidervalue < 10)
        {
            _onUnsummon.Invoke();
        }
    }
}
