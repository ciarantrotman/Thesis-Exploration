using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGrabbing : MonoBehaviour {

    public GameObject GrabOriginObject;
    public bool GrabEnabled = false;

	void Update () {

        if (GrabEnabled == true)
        {
            GrabOriginObject.SetActive(true);
        }

        else
        {
            GrabOriginObject.SetActive(false);
        }
    }
}
