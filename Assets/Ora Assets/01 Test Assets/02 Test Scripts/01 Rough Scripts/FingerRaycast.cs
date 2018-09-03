using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerRaycast : MonoBehaviour {

    public float RaycastDistance = 25.0f;
    private bool IsHover = false;

    private GameObject HoverObject;
    private GameObject ClonedObject;

    public float SmoothInSpeed = 1.0F;
    public float SmoothOutSpeed = 1.0F;

    public float LowerTarget = 0.0F;
    public float UpperTarget = 150.0f;

    public float ScaleFactor = .01f;

    void Update ()
    {
        Ray FingerRaycast = new Ray(transform.position, transform.forward);
        RaycastHit HitPoint;

        if (Physics.Raycast(FingerRaycast, out HitPoint, RaycastDistance) && HitPoint.transform.tag == "HapticObject")
        {
            IsHover = true;
            HoverObject = HitPoint.transform.gameObject;
            ClonedObject = Instantiate(HoverObject, HoverObject.transform.position, HoverObject.transform.rotation);

            ClonedObject.transform.localScale += new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);

            Debug.Log(transform.name + " hit " + ClonedObject.transform.name);
        }

        else
        {
            Destroy(ClonedObject);
        }
	}
}

/*
void FixedUpdate()
{
    Ray FingerRaycast = new Ray(transform.position, transform.forward);
    RaycastHit HitPoint;

    if (Physics.Raycast(FingerRaycast, out HitPoint, RaycastDistance) && HitPoint.transform.tag == "HapticObject")
    {
        IsHover = true;
        HoverObject = HitPoint.collider.gameObject;
        StartCoroutine(HoverStart());

        Debug.Log(transform.name + " hit " + HitPoint.transform.name);
    }

    else
    {
        IsHover = false;
        StartCoroutine(HoverEnd());
    }
}

private IEnumerator HoverStart()
{
    while (IsHover == true)
    {
        ClonedObject = Instantiate(HoverObject, HoverObject.transform.position, HoverObject.transform.rotation);
        HoverObject.transform.localScale += new Vector3(1f, 1f, 1f);
    }

    yield return null;
}

private IEnumerator HoverEnd()
{
    Destroy(ClonedObject);
    yield return null;
}
*/