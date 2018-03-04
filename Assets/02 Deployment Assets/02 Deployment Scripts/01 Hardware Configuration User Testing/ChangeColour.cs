using System.Collections;
using UnityEngine;

public class ChangeColour : MonoBehaviour {

    public Material ActiveMaterial;
    private Material ActiveMaterialSafe;
    public GameObject TargetObject;

    private void Start()
    {
        ActiveMaterialSafe = Instantiate(ActiveMaterial);
    }

    public void ColourChange()
    {
        ActiveMaterialSafe.color = Color.red;
	}
}
