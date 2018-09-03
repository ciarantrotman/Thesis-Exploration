using UnityEngine;
using System.Collections;

public class ActivateUI : MonoBehaviour
{
    public GameObject ActivatedUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ActivatedUI.SetActive(true);
    }
}