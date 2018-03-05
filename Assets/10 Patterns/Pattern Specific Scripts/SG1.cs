using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG1 : MonoBehaviour {

    void Update()
    {
        if (GameObject.Find("Manual Input Controller").GetComponent<IndirectGrab>().IndirectSelectionState == true)
        {
            Debug.Log("Pressed");
        }
    }
}
