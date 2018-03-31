using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalProgramController : MonoBehaviour {

    private Camera HMD;
    private Renderer TriggerRenderer;
    private Collider TriggerCollider;
    private GameObject UserHead;
    private GameObject EnvironmentalProgram;
    private int LoopCount = 0;

    void Start ()
    {
        EnvironmentalProgram = gameObject;
        TriggerRenderer = EnvironmentalProgram.GetComponent<Renderer>();
        TriggerCollider = EnvironmentalProgram.GetComponent<Collider>();
        UserHead = GameObject.Find("HMD Camera");
        HMD = UserHead.GetComponent<Camera>();
        HMD.cullingMask = ~(1 << LayerMask.NameToLayer("Environmental Program")); // Render everything *except* Environmental Programs
    }
    private void Update()
    {
        //Debug.Log(transform.parent.name);   
    }
    #region Collision Based
    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == UserHead.transform.name)
        {
            EnvironmentalProgram.transform.parent = UserHead.transform;
            StartCoroutine("EnvironmentalLaunch");
        }
        if (collider.GetComponent<Collider>().name == "Environmental Program - Position Marker")
        {
            EnvironmentalProgram.transform.parent = collider.transform;
            //transform.GetComponent<Rigidbody>().isKinematic = true;
        }
        
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == UserHead.transform.name)
        {
            HMD.cullingMask = ~(1 << LayerMask.NameToLayer("Environmental Program")); // Switch off Environmental Programs, leave others as-is
            HMD.cullingMask |= (1 << LayerMask.NameToLayer("Real World Objects")); // Switch on Real World Objects, leave others as-is
            TriggerRenderer.enabled = true;
            EnvironmentalProgram.transform.parent = null;
            LoopCount = 0;
        }
        if (collider.GetComponent<Collider>().name == "Environmental Program - Position Marker")
        {
            EnvironmentalProgram.transform.parent = null;
            //transform.GetComponent<Rigidbody>().isKinematic = false;
        }
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
        yield return new WaitForSeconds(1);
        LoopCount++;
        if (LoopCount > 0)
        {
            TriggerCollider.enabled = true;
        }
    }
    #endregion
    #region Trigger Based
    public void LaunchEnvironmental()
    {
        HMD.cullingMask |= (1 << LayerMask.NameToLayer("Environmental Program")); // Switch on Environmental Programs, leave others as-is
        HMD.cullingMask = ~(1 << LayerMask.NameToLayer("Real World Objects")); // Switch off Real World Objects, leave others as-is
    }

    public void CloseEnvironmental()
    {
        HMD.cullingMask = ~(1 << LayerMask.NameToLayer("Environmental Program")); // Switch off Environmental Programs, leave others as-is
        HMD.cullingMask |= (1 << LayerMask.NameToLayer("Real World Objects")); // Switch on Real World Objects, leave others as-is
    }
    #endregion
    public void OnGrab()
    {
        Debug.Log("Full Tiddy");
        EnvironmentalProgram.transform.parent = null;
    }
}
