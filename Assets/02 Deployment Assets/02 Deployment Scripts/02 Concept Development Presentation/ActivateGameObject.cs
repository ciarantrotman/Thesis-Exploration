using UnityEngine;
using System.Collections;

public class ActivateGameObject : MonoBehaviour
{
    private bool PressState = false;

    public GameObject TargetObject;

    public GameObject DeactivatedObjectOne;
    public GameObject DeactivatedObjectTwo;


    public void ToggleTargetObjectActive()
    {
        if (PressState == false)
        {
            TargetObject.SetActive(true);
            PressState = true;
        }

        else if (PressState == true)
        {
            TargetObject.SetActive(false);
            PressState = false;
        }
    }

    public void ToggleTargetObjectDeactive()
    {
        if (PressState == false)
        {
            TargetObject.SetActive(false);
            PressState = true;
        }

        else if (PressState == true)
        {
            TargetObject.SetActive(true);
            PressState = false;
        }
    }

    public void DeactivateObjects()
    {
        DeactivatedObjectOne.SetActive(false);
        DeactivatedObjectTwo.SetActive(false);
        //PressState = false;
    }
}