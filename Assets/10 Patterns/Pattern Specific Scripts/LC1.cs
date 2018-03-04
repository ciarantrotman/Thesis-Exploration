using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LC1 : MonoBehaviour {

    public GameObject ContextualMenu;
	
	public void LaunchContextualMenu()
    {
        ContextualMenu.SetActive(true);
    }

    public void LaunchContextual()
    {
        ContextualMenu.SetActive(false);
    }
}
