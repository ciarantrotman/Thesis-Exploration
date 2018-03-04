using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGrabTest : MonoBehaviour {

    public float ReachDistance = 100.0f;
    private GameObject InventoryObject;

    private GameObject OriginalParent;
    public bool Grabbing;

    private void Start()
    {
        InventoryObject = gameObject;
    }

    void Update ()
    {
        Ray GrabRay = new Ray(transform.position, transform.forward);
        RaycastHit HitPoint;

        while (Input.GetKeyDown(KeyCode.Space))
        {
            Grabbing = true;
        }

        if (Physics.Raycast(GrabRay, out HitPoint, ReachDistance) && HitPoint.transform.tag == "GrabObject" && Grabbing == true)
        {
            GetComponent<LineRenderer>().enabled = false;
            HitPoint.transform.parent = InventoryObject.transform;
        }

        if (Grabbing == false)
        {
            HitPoint.transform.parent = null;
            GetComponent<LineRenderer>().enabled = true;
        }
    }
}