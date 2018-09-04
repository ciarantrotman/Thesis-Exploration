using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalMenu : MonoBehaviour 
{
    public void Temp()
    {
        transform.position = GameObject.Find("IndexTip").transform.position;
    }
}
