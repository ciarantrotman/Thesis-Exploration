using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnvironmentalProgramController : MonoBehaviour {

    private Camera HMD;
    private Renderer TriggerRenderer;
    private Collider TriggerCollider;
    private GameObject UserHead;
    private GameObject EnvironmentalProgram;
    private int LoopCount = 0;

    [Header("Environmental Program Events")]
    public UnityEvent _onLaunch;
    public UnityEvent _onClose;

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
        if (collider.GetComponent<Collider>().name == "HMD Camera")
        {
            _onLaunch.Invoke();
            EnvironmentalProgram.transform.parent = UserHead.transform;
            StartCoroutine("EnvironmentalLaunch");
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Collider>().name == "HMD Camera")
        {
            _onClose.Invoke();
            HMD.cullingMask = ~(1 << LayerMask.NameToLayer("Environmental Program")); // Switch off Environmental Programs, leave others as-is
            HMD.cullingMask |= (1 << LayerMask.NameToLayer("Real World Objects")); // Switch on Real World Objects, leave others as-is
            TriggerRenderer.enabled = true;
            EnvironmentalProgram.transform.parent = null;
            LoopCount = 0;
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
        EnvironmentalProgram.transform.parent = null;
    }
}
