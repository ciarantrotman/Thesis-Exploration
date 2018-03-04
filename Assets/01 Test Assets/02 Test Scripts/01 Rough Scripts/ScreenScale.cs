using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScale : MonoBehaviour {

	void Start () {
		
	}

    public void ScaleOnPress()
    {
        transform.localScale += new Vector3(1, 1, 0);
    }

	void Update () {
		
	}
}
