using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CD3 : MonoBehaviour {

    private GameObject Self;

    void Start()
    {
        Self = gameObject;
    }

    void OnTriggerEnter(Collider collider)
    {
        Self.SetActive(false);
    }
}
