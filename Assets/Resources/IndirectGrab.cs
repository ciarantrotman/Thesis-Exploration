using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Uniduino;

public class IndirectGrab : MonoBehaviour
{
    #region Private Variables
    private Arduino arduino;

    private int InputPinValue;                      // value being fed from the Arduino
    private int ButtonPressValue;                   // value of InputPin when button is pressed
    private int ReleasePressValue;                  // value of InputPin when button is depressed
    private int InteractionType = 0;                // the int that determines the case for the main switchcase
    private int ShellState = 0;                     // the int that determines the case for the shell switchcase

    private float ReachDistance = 100.0f;           // how far the raycast for grabbing reaches
    private float SmoothVelocity = 1.0f;            // part of the highlight effect
    private float HoverScaleDynamic = 1.0f;         // determines the mathf.smoothdamp for the highlight effect
    private float LerpTime = 15.0f;                 // part of the Lerp logic
    private float CurrentLerpTime = 0.0f;           // part of the Lerp logic

    private GameObject TargetLocation;              // the object that is lerped to when grabbing
    private GameObject ClonedObject;                // this will be instantiated to create the highlight effect
    private GameObject SelectedObject;              // the object being hit by the raycast
    private GameObject LeapMotionRig;               // this is the Leap Motion Rig
    private GameObject TeleportTarget;              // this is a dynamic object that you teleport to
    private GameObject EgocentricOrigin;            // the main object that is rubber banded to you
    private GameObject GazeController;
    private GameObject ManualController;
    private GameObject[] ShellParents;               // an array of objects that will be activated on a trigger
    private LineRenderer RaycastLineRender;         // the linerenderer that shows where the user will act

    private ObjectMass ObjectMassScript;            // the script that determines the responsiveness of grabbed objects

    [HideInInspector]
    public bool LerpState = false;                 // determines whether to initialise the grabbing Lerp
    private bool ShellActive = false;               // TEMP
    private bool Mode;
    [HideInInspector]
    public bool ButtonPress = false;         // this is reffered to from other scripts to trigger events
    [HideInInspector]
    public bool IndirectSelectionState = true;      // this is reffered to from other scripts to trigger selection
    #endregion
    #region Inspector and Public Variables
    [Header("Grab Settings")]
    [Space(5)]
    public int InputPin = 2;                        // what pin on the Arduino is the button connected to
    public bool IsInputInverted = false;            // change this based on the wiring of the button
    public bool LineRendererEnabled = false;            // enable line renderer?

    [Header("Grab Physics")]
    [Space(5)]
    public float DefaultObjectMass = 5.0f;          // the responsiveness of a grabbed object

    [Header("Highlight Settings")]
    [Space(5)]
    public bool HighlightEnabled = true;
    [Space(5)]
    public float HoverScale = 1.1f;                 // how large the highlight effect is
    public float ScaleUpTime = .15f;                // how long it will take to highlight
    public float ScaleDownTime = .75f;              // how long it will take to reset
    [Space(5)]
    public Material HighlightMaterial;              // the material of the highlight effect

    [Header("Teleport Settings")]
    [Space(5)]
    public bool TeleportEnabled = true;

    [Header("Indirect Selection Settings")]
    [Space(5)]
    public bool IndirectSelectionEnabled = true;

    [Header("Shell Settings")]
    [Space(5)]
    public bool ShellEnabled = true;
    #endregion 

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
        TargetLocation = GameObject.Find("Target Location");
        LeapMotionRig = GameObject.Find("Leap Motion Rig");
        TeleportTarget = GameObject.Find("Teleport Target");
        EgocentricOrigin = GameObject.Find("Egocentric Content Origin");
        GazeController = GameObject.Find("Gaze Controller");
        ManualController = GameObject.Find("Manual Controller");
        RaycastLineRender = LineRendererEnabled ? GetComponent<LineRenderer>() : null;
        ButtonPressValue = IsInputInverted ? 1 : 0;
        ReleasePressValue = IsInputInverted ? 1 : 0;
    }
    void ConfigurePins()
    {
        arduino.pinMode(InputPin, PinMode.INPUT);
        arduino.reportDigital((byte)(InputPin / 8), 1);
    }
    void Update()
    {
        ButtonPress = (InputPinValue == ButtonPressValue) ? ButtonPress = true : ButtonPress = false;
        InteractionType = 0; 
        Ray GrabRay = new Ray(transform.position, transform.forward);
        RaycastHit HitPoint;
        InputPinValue = arduino.digitalRead(InputPin);
        #region Indirect Grab       | 1
        if (Physics.Raycast(GrabRay, out HitPoint, ReachDistance, LayerMask.NameToLayer("IgnoreIndirectGrab")) && HitPoint.transform.tag == "GrabObject")
        {
            InteractionType = 1;
            SelectedObject = HitPoint.transform.gameObject;
            RaycastLineRender.enabled = true;
            RaycastLineRender.useWorldSpace = true;
            Vector3 StartPoint = transform.position;
            Vector3 EndPoint = SelectedObject.transform.position;
            RaycastLineRender.SetPosition(0, StartPoint);
            RaycastLineRender.SetPosition(1, EndPoint);
        }
        #endregion
        #region Teleportation       | 2
        if (Physics.Raycast(GrabRay, out HitPoint, ReachDistance, LayerMask.NameToLayer("IgnoreIndirectGrab")) && HitPoint.transform.tag == "TeleportLocation")
        {
            if (TeleportEnabled == true)
            {
                InteractionType = 2;
                SelectedObject = HitPoint.transform.gameObject;
                RaycastLineRender.enabled = true;
                RaycastLineRender.useWorldSpace = true;
                Vector3 StartPoint = transform.position;
                Vector3 EndPoint = SelectedObject.transform.position;
                RaycastLineRender.SetPosition(0, StartPoint);
                RaycastLineRender.SetPosition(1, EndPoint);
                SelectedObject.GetComponent<BlendshapeAnimation>().OnTriggerStart();
            }
        }
        #endregion
        #region Selection           | 3
        if (Physics.Raycast(GrabRay, out HitPoint, ReachDistance, LayerMask.NameToLayer("IgnoreIndirectGrab")) && HitPoint.transform.tag == "SelectableUI")
        {
            if (IndirectSelectionEnabled == true)
            {
                InteractionType = 3;
                SelectedObject = HitPoint.transform.gameObject;
                RaycastLineRender.enabled = true;
                RaycastLineRender.useWorldSpace = true;
                Vector3 StartPoint = transform.position;
                Vector3 EndPoint = SelectedObject.transform.position;
                RaycastLineRender.SetPosition(0, StartPoint);
                RaycastLineRender.SetPosition(1, EndPoint);
            }
        }
        #endregion
        #region Shell Trigger       | 4
        // wait until after the prototyping stage
        #endregion
        switch (InteractionType)
        {
            case 1:
                #region Indirect Grab
                SelectedObject = HitPoint.transform.gameObject;
                if (ClonedObject == null && HighlightEnabled == true)
                {
                    ClonedObject = Instantiate(HitPoint.transform.gameObject, HitPoint.transform.position, HitPoint.transform.rotation);
                    ClonedObject.GetComponent<Collider>().enabled = false;
                    Component[] ChildColliders = ClonedObject.GetComponentsInChildren(typeof(Collider));
                    foreach (Collider ChildCollider in ChildColliders)
                    {
                        ChildCollider.enabled = false;
                    }
                    ClonedObject.transform.parent = HitPoint.transform;
                    Renderer CloneRenderer = ClonedObject.GetComponent<Renderer>();
                    if (HighlightMaterial == null)
                    {
                        HighlightMaterial = Resources.Load("HighlightMatBackup", typeof(Material)) as Material;
                    }
                    Component[] ChildRenderers = ClonedObject.GetComponentsInChildren(typeof(Renderer));
                    foreach (Renderer ChildRenderer in ChildRenderers)
                    {
                        ChildRenderer.material = HighlightMaterial;
                        ChildRenderer.material.renderQueue = 3000;
                    }
                    if (CloneRenderer != null)
                    {
                        CloneRenderer.material = HighlightMaterial;
                        CloneRenderer.material.renderQueue = 3000;
                    }
                }
                if (ClonedObject != null && HighlightEnabled == true)
                {
                    HoverScaleDynamic = (Mathf.SmoothDamp(HoverScaleDynamic, HoverScale, ref SmoothVelocity, ScaleUpTime));
                }
                if (ButtonPress == true)
                {
                    if (LerpState == false)
                    {
                        this.SelectedObject = HitPoint.transform.gameObject;
                        this.SelectedObject.GetComponent<BoxCollider>().isTrigger = false;
                        if (this.SelectedObject.GetComponent<ObjectMass>() != null)
                        {
                            ObjectMassScript = this.SelectedObject.GetComponent<ObjectMass>();
                            LerpTime = ObjectMassScript.DigitalObjectMass;
                        }
                        if (this.SelectedObject.GetComponent<ObjectMass>() == null)
                        {
                            LerpTime = DefaultObjectMass;
                        }
                        TargetLocation.transform.position = SelectedObject.transform.position;
                        LerpState = true;
                        RaycastLineRender.enabled = false;
                    }
                    if (ClonedObject != null && HighlightEnabled == true)
                    {
                        HoverScaleDynamic = (Mathf.SmoothDamp(HoverScaleDynamic, .95f, ref SmoothVelocity, .1f));
                    }
                    if (HoverScaleDynamic < 1 && HighlightEnabled == true)
                    {
                        Destroy(ClonedObject);
                    }
                }
                if (ButtonPress == false)
                {
                    //RaycastLineRender.enabled = true;
                    SelectedObject = null;
                    if (this.SelectedObject != null)
                    {
                        this.SelectedObject.GetComponent<BoxCollider>().isTrigger = true;
                    }
                    LerpState = false;
                    CurrentLerpTime = 0;
                }
                break;
            #endregion
            case 2:
                #region Teleportation
                TeleportTarget = HitPoint.transform.gameObject;
                Vector3 TeleportPosition = LeapMotionRig.transform.position;
                Vector3 TeleportTargetPosition = TeleportTarget.transform.position;
                if (ButtonPress == true)
                {
                    TeleportPosition.x = TeleportTargetPosition.x;
                    TeleportPosition.z = TeleportTargetPosition.z;
                    LeapMotionRig.transform.position = TeleportPosition;
                }
                break;
            #endregion
            case 3:
                #region Selection
                IndirectSelectionState = ButtonPress ? true : false;
                break;
                #endregion
            case 4:
                #region Shell
                break;
                #endregion
            default:
                #region Default;
                RaycastLineRender.enabled = false;
                RaycastLineRender.useWorldSpace = false;
                if (ClonedObject != null && HighlightEnabled == true)
                {
                    HoverScaleDynamic = (Mathf.SmoothDamp(HoverScaleDynamic, .95f, ref SmoothVelocity, ScaleDownTime));
                }
                if (HoverScaleDynamic < 1 && HighlightEnabled == true)
                {
                    Destroy(ClonedObject);
                }
                if (SelectedObject != null && SelectedObject.GetComponent<BlendshapeAnimation>() != null)
                {
                    SelectedObject.GetComponent<BlendshapeAnimation>().OnTriggerEnd();
                }
                if (ButtonPress == true && ShellEnabled == true)
                {
                    StartCoroutine(ShellLaunch());
                }
                break;
                #endregion
        }
    }
    private void LateUpdate()
    {
        if (ClonedObject != null && HighlightEnabled == true)
        {
            ClonedObject.transform.localScale = new Vector3(HoverScaleDynamic, HoverScaleDynamic, HoverScaleDynamic);
            ClonedObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        }           // handles the scale down of highlighted objects
        if (LerpState == true)
        {
            CurrentLerpTime += Time.deltaTime;
            if (CurrentLerpTime >= 1)
            {
                CurrentLerpTime = 0;
            }
            float JourneyPercentage = CurrentLerpTime / LerpTime;
            SelectedObject.transform.position = Vector3.Lerp(SelectedObject.transform.position, TargetLocation.transform.position, JourneyPercentage);
        }                                          // handles the actual grab mechanism
    }
    private IEnumerator ShellLaunch()
    {
        //Debug.Log(ShellParents);
        ShellActive = ShellActive ? false : true;
        if (ShellParents == null)
        {
            ShellParents = GameObject.FindGameObjectsWithTag("ShellObject");
        }
        foreach (GameObject ShellParent in ShellParents)
        {
            ShellParent.SetActive(ShellActive);
        }
        yield return new WaitForSeconds(3);
    }
    
    #region Modality Controller
    /*public void Gaze()
    {
        Mode = true;
        while (Mode == true)
        {
            transform.position = ModalityControllerEnabled ? GazeController.transform.position : transform.position;
            transform.localRotation = ModalityControllerEnabled ? GazeController.transform.rotation : transform.localRotation;
        }
    }

    public void Manual()
    {
        Mode = false;
        while (Mode == false)
        {
            transform.position = ModalityControllerEnabled ? ManualController.transform.position : transform.position;
            transform.localRotation = ModalityControllerEnabled ? ManualController.transform.rotation : transform.localRotation;
        }
    }*/
    #endregion
}