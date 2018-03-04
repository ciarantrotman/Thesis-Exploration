using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeTrigger : MonoBehaviour {

    [Header("Gaze Trigger Settings")]
    [Space(5)]
    public bool ActiveOnGaze;
    public bool DeactiveOffGaze;
    private int ReachDistance = 100;
    private GameObject GazeObject;

	void Update ()
    {
        Ray GazeRay = new Ray(transform.position, transform.forward);
        RaycastHit HitPoint;

        if (Physics.Raycast(GazeRay, out HitPoint, ReachDistance) && HitPoint.transform.tag == "GazeTrigger" && ActiveOnGaze == true)
        {
            GazeObject = HitPoint.transform.gameObject;
            GazeObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        /*
        else if (GazeObject != null && DeactiveOffGaze == true)
        {
            GazeObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        */
    }
}
