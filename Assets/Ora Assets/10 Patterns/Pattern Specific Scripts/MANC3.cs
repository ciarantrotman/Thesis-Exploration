using UnityEngine;
using System.Collections;

public class MANC3 : MonoBehaviour
{
    public Transform TargetObject;

    void Update()
    { 
        transform.LookAt(TargetObject);
    }
}