using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighlightOnHover : MonoBehaviour
{

    private GameObject ClonedObject;
    public float HoverScale = 1.1f;
    public float HighlightSpeed = 1;
    public Material HighlightMaterial;
    private Material OriginalMaterial;
    private float SmoothVelocity = 1.0f;

    public GameObject ObjectToClone;
    private float HoverScaleDynamic;


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (ClonedObject == null)
            {
                ClonedObject = Instantiate(ObjectToClone, ObjectToClone.transform.position, ObjectToClone.transform.rotation);
                Renderer CloneRenderer = ClonedObject.GetComponent<Renderer>();

                if (HighlightMaterial == null)
                    {
                        HighlightMaterial = Resources.Load("HighlightMatBackup", typeof(Material)) as Material;
                    }

                Component[] ChildRenderers = ClonedObject.GetComponentsInChildren(typeof(Renderer));

                foreach (Renderer ChildRenderer in ChildRenderers)
                    {
                        ChildRenderer.material = HighlightMaterial;
                        ChildRenderer.material.renderQueue = 3000;
                    }

                if (CloneRenderer != null)
                {
                    CloneRenderer.material = HighlightMaterial;
                    CloneRenderer.material.renderQueue = 3000;
                }
            }

            else
            {
                HoverScaleDynamic = (Mathf.SmoothDamp(HoverScaleDynamic, HoverScale, ref SmoothVelocity, HighlightSpeed));
            }
        }

        else
        {
            if (ClonedObject != null)
            {
                HoverScaleDynamic = (Mathf.SmoothDamp(HoverScaleDynamic, .95f, ref SmoothVelocity, HighlightSpeed));
            }

            if (HoverScaleDynamic < 1)
            {
                Destroy(ClonedObject);
            }
        }
    }

    private void LateUpdate()
    {
        if (ClonedObject != null)
        {
            ClonedObject.transform.localScale = new Vector3(HoverScaleDynamic, HoverScaleDynamic, HoverScaleDynamic);
        }
    }
}

/*
public class HighlightOnHover : MonoBehaviour
{
    public float HoverScale = 1.1f;
    public float HighlightSpeed = 1;
    private float SmoothVelocity = 1.0f;

    public GameObject ObjectToClone;
    private float HoverScaleDynamic;


    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            HoverScaleDynamic = (Mathf.SmoothDamp(HoverScaleDynamic, HoverScale, ref SmoothVelocity, HighlightSpeed));
        }
        
        else
        {
            HoverScaleDynamic = (Mathf.SmoothDamp(HoverScaleDynamic, .95f, ref SmoothVelocity, HighlightSpeed));
        }
    }

    private void LateUpdate()
    {
        ObjectToClone.transform.localScale = new Vector3(HoverScaleDynamic, HoverScaleDynamic, HoverScaleDynamic);
    }
}
*/
