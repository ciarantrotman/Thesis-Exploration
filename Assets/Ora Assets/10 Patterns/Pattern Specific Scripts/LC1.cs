using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LC1 : MonoBehaviour {

    public GameObject ContextualMenu;	
    
    void Update()
    {
        if (GameObject.Find("Manual Input Controller").GetComponent<IndirectGrab>().IndirectSelectionState == true)
        {
            Invoke("LaunchContextualMenu", 0);
        }
    }

	public void LaunchContextualMenu()
    {
        ContextualMenu.SetActive(true);
    }

    public void LaunchContextual()
    {
        ContextualMenu.SetActive(false);
    }
}
