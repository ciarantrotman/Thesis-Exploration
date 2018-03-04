using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    public GameObject FollowerObject;
    public GameObject FollowedObject;
	
	void Update () {
        FollowerObject.transform.position = FollowedObject.transform.position;
	}
}
