using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LD3 : MonoBehaviour {

    private GameObject EgocentricOrigin;
    public GameObject Shell;

    void Start()
    {
        EgocentricOrigin = GameObject.Find("Egocentric Content Origin");
    }

public void DisableShell()
    {
        Shell.SetActive(false);
    }

public void MakeEgocentric()
    {
        transform.localScale = new Vector3(3, 3, 3);
        transform.parent = EgocentricOrigin.transform;
    }
}
