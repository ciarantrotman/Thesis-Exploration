using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX1 : MonoBehaviour {

    private Rigidbody rigidbody;
    public GameObject TransitionContent;
    private GameObject UserCenter;
    private GameObject EgocentricContentOrigin;
    public float TransitionThreshold;
    private float ContentDistance;

    public void Start()
    {
        rigidbody = TransitionContent.GetComponent<Rigidbody>();
        EgocentricContentOrigin = GameObject.Find("Egocentric Content Origin");
        UserCenter = GameObject.Find("User Center");
    }

    public void Update()
    {
        Debug.Log(ContentDistance + " | " + TransitionThreshold);
        ContentDistance = Vector3.Distance(TransitionContent.transform.position, UserCenter.transform.position);
        if (ContentDistance < TransitionThreshold)
        {
            rigidbody.drag = 3;
            TransitionContent.transform.parent = EgocentricContentOrigin.transform;
        }
        else
        {
            rigidbody.drag = 100;
            TransitionContent.transform.parent = null;
        }
    }
}
