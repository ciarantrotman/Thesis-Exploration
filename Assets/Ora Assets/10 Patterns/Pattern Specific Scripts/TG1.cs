using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TG1 : MonoBehaviour
{
    public GameObject TransitionContent;
    private GameObject UserCenter;
    private GameObject EgocentricContentOrigin;
    public float TransitionThreshold;
    private float ContentDistance;

    public void Start()
    {
        EgocentricContentOrigin = GameObject.Find("Egocentric Content Origin");
        UserCenter = GameObject.Find("User Center");
    }

    public void Update()
    {
        Debug.Log(ContentDistance + " | " + TransitionThreshold);
        ContentDistance = Vector3.Distance(TransitionContent.transform.position, UserCenter.transform.position);
        if (ContentDistance < TransitionThreshold)
        {
            TransitionContent.transform.parent = EgocentricContentOrigin.transform;
        }
        else
        {
            TransitionContent.transform.parent = null;
        }
    }
}
