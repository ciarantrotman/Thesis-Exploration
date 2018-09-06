using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBehaviour : MonoBehaviour
{
    private DirectionalMenu _dirMenu;
    [HideInInspector]
    public float CtrlNodeDistance;

    public enum Node{UpRight, Up, UpLeft, DownLeft, Forward, Back}
    public Node node;
    
    private void Start()
    {
        _dirMenu = GameObject.Find("DirectionalMenu").GetComponent<DirectionalMenu>();
    }

    private void Update()
    {
        if (_dirMenu.MenuNodes.Contains(transform.gameObject) == false)
            _dirMenu.MenuNodes.Add(transform.gameObject);
    }

    public void NodeTriggered()
    {
        if (GameObject.Find("HMD Camera").GetComponent<ObjectSelection>().LastActiveObject == null) return;
            GameObject.Find("HMD Camera").GetComponent<ObjectSelection>().LastActiveObject.GetComponent<ObjectQuickActions>().Invoke(node.ToString(), 0);
    }
}
