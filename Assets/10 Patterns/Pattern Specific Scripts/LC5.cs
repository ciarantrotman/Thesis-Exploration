using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LC5 : MonoBehaviour {

    public GameObject LineRendererEnd;

	void Update ()
    {
        Vector3 StartPoint = transform.position;
        Vector3 EndPoint = LineRendererEnd.transform.position;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, StartPoint);
        lineRenderer.SetPosition(1, EndPoint);
    }
}
