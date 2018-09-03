using UnityEngine;
using System.Collections;

public class GrabObjectDEBUG : MonoBehaviour
{
    bool grabbed = false;
    bool collided = false;

    public GameObject ChildObject;
    public GameObject ParentObject;
    public GameObject RaycastingObject;
    public GameObject LineRenderer;
    public GameObject Highlight;


    void OnTriggerStay (Collider TargetObject)
    {
        if (TargetObject.gameObject.name == RaycastingObject.transform.name)
        {
            collided = true;
            //Debug.Log(TargetObject.transform.name);
        }
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow) && collided == true) //grab
        {
            ChildObject.transform.parent = ParentObject.transform;
            LineRenderer.SetActive(false);
            Debug.Log("SPACE BOI");
            //Highlight.active = true;
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            ChildObject.transform.parent = null;
            LineRenderer.SetActive(true);
            //Highlight.active = false;
            collided = false;
            grabbed = false;
        }
    }
}