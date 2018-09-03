using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE3 : MonoBehaviour
{

    private Camera HMD;
    private Renderer TriggerRenderer;
    private Collider TriggerCollider;
    private GameObject UserHead;
    private GameObject Trigger;
    private int LoopCount = 0;

    void Start()
    {
        Trigger = gameObject;
        TriggerRenderer = Trigger.GetComponent<Renderer>();
        TriggerCollider = Trigger.GetComponent<Collider>();
        UserHead = GameObject.Find("HMD Camera");
        HMD = UserHead.GetComponent<Camera>();
        HMD.cullingMask |= (1 << LayerMask.NameToLayer("Environmental Program")); // Switch on Environmental Programs, leave others as-is
        HMD.cullingMask = ~(1 << LayerMask.NameToLayer("Real World Objects")); // Switch off Real World Objects, leave others as-is
    }

    #region Collision Based
    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == UserHead.transform.name)
        {
            Debug.Log("Head Hit");
            StartCoroutine("EnvironmentalClose");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        HMD.cullingMask |= (1 << LayerMask.NameToLayer("Environmental Program")); // Switch on Environmental Programs, leave others as-is
        HMD.cullingMask = ~(1 << LayerMask.NameToLayer("Real World Objects")); // Switch off Real World Objects, leave others as-is
        LoopCount = 0;
    }

    IEnumerator EnvironmentalClose()
    {
        HMD.cullingMask = ~(1 << LayerMask.NameToLayer("Environmental Program")); // Switch off Environmental Programs, leave others as-is
        HMD.cullingMask |= (1 << LayerMask.NameToLayer("Real World Objects")); // Switch on Real World Objects, leave others as-is
        if (LoopCount == 0)
        {
            TriggerCollider.enabled = false;
        }
        yield return new WaitForSeconds(1);
        LoopCount++;
        Debug.Log(LoopCount);
        if (LoopCount > 0)
        {
            TriggerCollider.enabled = true;
        }
    }
    #endregion
}
