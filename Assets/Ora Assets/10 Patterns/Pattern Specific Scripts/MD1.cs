using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MD1 : MonoBehaviour
{
    public GameObject LeftHand;
    public GameObject RightHand;
    private float HandDistance;

    public void Update()
    {
        HandDistance = Vector3.Distance(LeftHand.transform.position, RightHand.transform.position);
    }

    public void Scaling()
    {
        transform.localScale = new Vector3(HandDistance, HandDistance, HandDistance);
    }
}
