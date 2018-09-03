using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MI1 : MonoBehaviour
{

    public GameObject HandleOrigin;
    public GameObject Handle;
    private float HandleDistance;
    private float ScaleFactor;
    private float InitialDistance;

    public void Start()
    {
        InitialDistance = Vector3.Distance(HandleOrigin.transform.position, Handle.transform.position);
    }

    public void Update()
    {
        Debug.Log(ScaleFactor);
        HandleDistance = Vector3.Distance(HandleOrigin.transform.position, Handle.transform.position);
        ScaleFactor = (InitialDistance + HandleDistance);
        if (GameObject.Find("Manual Input Controller").GetComponent<IndirectGrab>().LerpState == true)
        {
            transform.localScale = new Vector3(ScaleFactor, ScaleFactor, ScaleFactor);
        }
        else
        {
            transform.localScale = transform.localScale;
        }
    }
}
