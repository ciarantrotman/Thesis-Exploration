using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAnimation : MonoBehaviour {

    public float RaycastDistance = 25.0f;

    void Update()
    {
        Ray FingerRaycast = new Ray(transform.position, transform.forward);
        RaycastHit HitPoint;

        if (Physics.Raycast(FingerRaycast, out HitPoint, RaycastDistance) && HitPoint.transform.tag == "AnimatedObject")
        {
            StartCoroutine(HitPoint.transform.GetComponent<BlendshapeAnimationTest>().HoverStart());
        }

        else
        {
            StartCoroutine(HitPoint.transform.GetComponent<BlendshapeAnimationTest>().HoverEnd());
        }
    }
}