using UnityEngine;
using System.Collections;

public class DeactivateGameObject : MonoBehaviour
{
    public GameObject TargetObjectOne;
    public GameObject TargetObjectTwo;
    public GameObject TargetObjectThree;


    public void DeactivateObjects()
    {
        TargetObjectOne.SetActive(false);
        TargetObjectTwo.SetActive(false);
        TargetObjectThree.SetActive(false);
    }
}