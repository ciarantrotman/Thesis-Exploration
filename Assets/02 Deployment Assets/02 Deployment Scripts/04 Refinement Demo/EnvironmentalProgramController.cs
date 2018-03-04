using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalProgramController : MonoBehaviour {

    private Camera HMD;
    private Renderer TriggerRenderer;
    private Collider TriggerCollider;
    private GameObject UserHead;
    private GameObject Trigger;
    private int LoopCount = 0;

    void Start ()
    {
        Trigger = gameObject;
        TriggerRenderer = Trigger.GetComponent<Renderer>();
        TriggerCollider = Trigger.GetComponent<Collider>();
        UserHead = GameObject.Find("HMD Camera");
        HMD = UserHead.GetComponent<Camera>();
        HMD.cullingMask = ~(1 << LayerMask.NameToLayer("Environmental Program")); // Render everything *except* Environmental Programs
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == UserHead.transform.name)
        {
            StartCoroutine("EnvironmentalLaunch");
        }
    }
	
    void OnTriggerExit(Collider collider)
    {
        HMD.cullingMask = ~(1 << LayerMask.NameToLayer("Environmental Program")); // Switch off Environmental Programs, leave others as-is
        HMD.cullingMask |= (1 << LayerMask.NameToLayer("Real World Objects")); // Switch on Real World Objects, leave others as-is
        TriggerRenderer.enabled = true;
        Trigger.transform.parent = null;
        LoopCount = 0;
    }

    IEnumerator EnvironmentalLaunch()
    {
        HMD.cullingMask |= (1 << LayerMask.NameToLayer("Environmental Program")); // Switch on Environmental Programs, leave others as-is
        HMD.cullingMask = ~(1 << LayerMask.NameToLayer("Real World Objects")); // Switch off Real World Objects, leave others as-is
        TriggerRenderer.enabled = false;
        if (LoopCount == 0)
        {
            TriggerCollider.enabled = false;
        }
        Trigger.transform.parent = UserHead.transform;
        yield return new WaitForSeconds(1);
        LoopCount++;
        Debug.Log(LoopCount);
        if (LoopCount > 0)
        {
            TriggerCollider.enabled = true;
        }
    }
}
