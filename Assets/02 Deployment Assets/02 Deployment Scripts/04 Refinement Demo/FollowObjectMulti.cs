using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectMulti : MonoBehaviour
{

    public bool FollowRotation = false;
    public bool FollowPosition = false;
    public GameObject FollowerObject;
    public GameObject FollowedObject;

    void Update()
    {
        FollowerObject.transform.position = FollowPosition ? FollowedObject.transform.position : FollowerObject.transform.position;
        FollowerObject.transform.localRotation = FollowRotation ? FollowedObject.transform.rotation : FollowerObject.transform.localRotation;
    }
}
