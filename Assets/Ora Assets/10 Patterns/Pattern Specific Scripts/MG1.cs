using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG1 : MonoBehaviour
{
    void Update()
    {
        if (GameObject.Find("Manual Input Controller").GetComponent<IndirectGrab>().LerpState == true)
        {
             transform.LookAt(GameObject.Find("HMD Camera").transform);
        }
    }
}
