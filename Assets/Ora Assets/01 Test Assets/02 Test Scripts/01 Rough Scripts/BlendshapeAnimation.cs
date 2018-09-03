using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendshapeAnimation : MonoBehaviour
{

    SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Blendshape Animation Settings")]
    [Space(5)]
    public bool ScriptOnGameobject = true;
    public GameObject ScriptedObject;
    public bool InvertedBlendshapes = false;
    [Space(5)]
    public int BlendshapeNumber = 1;
    public float BlendDuration = 1.0F;

    private float BlendVelocity = 0.0f;
    private float UpperTarget = 100.0F;
    private float LowerTarget = 0.0F;
    private float BlendValue = 0.0f;
    private bool Trigger = false;

    private float UpperLimitCutoff = 99.0f;
    private float LowerLimitCutoff = 1.0f;

    void Awake()
    {
        if (ScriptOnGameobject == false && ScriptedObject != null)
        {
            skinnedMeshRenderer = ScriptedObject.GetComponent<SkinnedMeshRenderer>();
        }
        else
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }
    }

    private void Start()
    {
        if (InvertedBlendshapes == true)
        {
            UpperTarget = 0.0f;
            LowerTarget = 100;

            UpperLimitCutoff = 1.0f;
            LowerLimitCutoff = 99.0f;
        }
    }

    public void OnTriggerStart()
    {
        Trigger = true;
        StartCoroutine(TriggerStart());
    }

    public IEnumerator TriggerStart()
    {
        while (Trigger == true && BlendValue < UpperLimitCutoff)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(BlendshapeNumber, BlendValue);
            BlendValue = (Mathf.SmoothDamp(BlendValue, UpperTarget, ref BlendVelocity, BlendDuration));
            yield return null;
        }

    }

    public void OnTriggerEnd()
    {
        Trigger = false;
        StartCoroutine(TriggerEnd());
    }

    public IEnumerator TriggerEnd()
    {
        while (BlendValue > LowerLimitCutoff)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(BlendshapeNumber, BlendValue);
            BlendValue = (Mathf.SmoothDamp(BlendValue, LowerTarget, ref BlendVelocity, BlendDuration));
            yield return null;
        }
    }
}

