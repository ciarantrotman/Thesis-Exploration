using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalObjectMat : MonoBehaviour {

	void Start ()
    {
        Renderer ObjectRenderer = GetComponent<Renderer>();

        Component[] ChildRenderers = GetComponentsInChildren(typeof(Renderer));

            foreach (Renderer ChildRenderer in ChildRenderers)
            {
                ChildRenderer.material.renderQueue = 3010;
            }

        if (ObjectRenderer != null)
        {
            ObjectRenderer.material.renderQueue = 3010;
        }
    }
}
