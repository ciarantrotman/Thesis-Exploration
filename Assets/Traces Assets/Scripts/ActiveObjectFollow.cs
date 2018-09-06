using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectFollow : MonoBehaviour
{
    private bool _follow;

    private void Update()
    {
        Debug.Log(GameObject.Find("HMD Camera").GetComponent<ObjectSelection>().LastActiveObject == null);
        transform.gameObject.SetActive(GameObject.Find("HMD Camera").GetComponent<ObjectSelection>().LastActiveObject == null);

        if (_follow == true)
            transform.position = GameObject.Find("HMD Camera").GetComponent<ObjectSelection>().LastActiveObject
                .transform.position;
    }

    public void FollowBegin()
    {
        _follow = true;
    }

    public void FollowEnd()
    {
        _follow = false;
    }
}


