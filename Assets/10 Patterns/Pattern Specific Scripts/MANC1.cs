using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MANC1 : MonoBehaviour {

    public GameObject Shell;

    void Update()
    {
        if (GameObject.Find("Manual Input Controller").GetComponent<IndirectGrab>().ButtonPress == true)
        {
            Shell.SetActive(true);
        }
    }
}
