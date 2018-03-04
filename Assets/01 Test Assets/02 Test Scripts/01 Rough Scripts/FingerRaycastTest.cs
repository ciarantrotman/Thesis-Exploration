using UnityEngine;
using System.Collections;

public class FingerRaycastTest : MonoBehaviour
{
    public GameObject TargetObject;

    public float RaycastDistance = 25.0f;

    void FixedUpdate()
    {
        Ray FingerRaycast = new Ray(transform.position, Vector3.forward);
        RaycastHit Hit;

        if (Physics.Raycast(FingerRaycast, out Hit, RaycastDistance))
        {
            Debug.Log("You did the thing");
        }

    }
}