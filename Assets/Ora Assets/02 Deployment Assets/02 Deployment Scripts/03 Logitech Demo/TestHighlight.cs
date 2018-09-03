using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHighlight : MonoBehaviour {

    private GameObject ClonedObject;
    private float HoverScale = 1.1f;
    private float HighlightSpeed = 1;
    private float SmoothVelocity = 1.0f;

    public GameObject ObjectToClone;
    public float HoverScaleDynamic = 1.1F;
    public int HoverDuration = 0;

	void Start ()
    {
        //HoverScaleDynamic = HoverScale;
    }
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (ClonedObject == null)
            {
                HoverDuration = 1;
                StartCoroutine(Highlight());
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

            HoverDuration = 0;
        }
	}

    private void LateUpdate()
    {
        if (ClonedObject != null)
        {
            ClonedObject.transform.localScale = new Vector3(HoverScaleDynamic, HoverScaleDynamic, HoverScaleDynamic);
        }
    }

    private IEnumerator Highlight()
    {
        if (HoverDuration == 1)
        {
            if (ClonedObject == null)
            {
                ClonedObject = Instantiate(ObjectToClone, ObjectToClone.transform.position, ObjectToClone.transform.rotation);
                HoverDuration++;
            }

            else
            {                
                HoverDuration++;
            }
        }

        if (HoverDuration > 1)
        {
            StartCoroutine(HighlightScale());
        }

        if (HoverDuration == 0)
        {
            yield return null;
        }

    }

    private IEnumerator HighlightScale()
    {
        if (HoverDuration == 0)
        {
            yield return null;
        }

        while (HoverScaleDynamic < HoverScale-.01)
        {
            Debug.Log(HoverScaleDynamic);
            HoverScaleDynamic = (Mathf.SmoothDamp(HoverScaleDynamic, HoverScale, ref SmoothVelocity, HighlightSpeed));
        }       
    }
}
