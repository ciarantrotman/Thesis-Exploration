using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LD1 : MonoBehaviour {

    /*void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "Shell Proxy")
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }*/

    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "Shell Proxy")
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
    }
}
