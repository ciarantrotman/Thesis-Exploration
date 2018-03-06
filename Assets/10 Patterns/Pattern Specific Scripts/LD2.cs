using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LD2 : MonoBehaviour {

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    /*void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "Shell Proxy")
        {
            rigidbody.drag = 100;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }*/

    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "Shell Proxy")
        {
            Debug.Log("Yeet");
            rigidbody.drag = 100;
            transform.localScale = new Vector3(3, 3, 3);
        }
    }
}
