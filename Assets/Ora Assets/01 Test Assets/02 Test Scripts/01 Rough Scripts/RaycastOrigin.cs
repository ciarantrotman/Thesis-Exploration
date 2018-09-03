/*using UnityEngine;

public class RaycastOrigin : MonoBehaviour
{
    void Update()
    {
        Vector3 raycast = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, raycast, 10))
        {
            Debug.Log(RaycastHit.name);
        }
    }
}*/

using UnityEngine;
using System.Collections;

public class RaycastOrigin : MonoBehaviour
{
    public GameObject RaycastOriginObject;

    int layerMask = 1 << 8;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            {
            RaycastHit hit;
            Ray RightHandRaycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Vector3 ray = RaycastOriginObject.transform.TransformDirection(Vector3.forward);
            //if (Physics.Raycast(RightHandRaycast, out hit))

            if (Physics.Raycast(RightHandRaycast, /*layerMask 8,*/ out hit))
                {
                Debug.Log(hit.transform.name);
                }
        }
    }
}