using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTest : MonoBehaviour
{

    public float RaycastDistance = 25.0f;

    void FixedUpdate()
    {
        Ray FingerRaycast = new Ray(transform.position, transform.forward);
        RaycastHit HitPoint;

        if (Physics.Raycast(FingerRaycast, out HitPoint, RaycastDistance) && HitPoint.transform.tag == "HapticObject")
        {
            Debug.Log(transform.name + " hit " + HitPoint.transform.name);
        }
    }
}
