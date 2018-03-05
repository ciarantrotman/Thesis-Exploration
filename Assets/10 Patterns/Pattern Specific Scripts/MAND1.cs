using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAND1 : MonoBehaviour
{
    private int ReachDistance = 100;
    private GameObject HoverActivatedObject;

    void Update()
    {
        Ray HoverTriggerRay = new Ray(transform.position, transform.forward);
        RaycastHit HitPoint;

        if (Physics.Raycast(HoverTriggerRay, out HitPoint, ReachDistance) && HitPoint.transform.tag == "HoverTrigger")
        {
            HoverActivatedObject = HitPoint.transform.gameObject;
            if (HoverActivatedObject.transform.GetChild(0) != null)
            HoverActivatedObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            if (HoverActivatedObject != null)
            HoverActivatedObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
