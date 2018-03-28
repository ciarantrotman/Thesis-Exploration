using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Uniduino;
using Leap.Unity.Animation;

public class IndirectGrab : MonoBehaviour
{
    #region Private Variables
    private Arduino arduino;

    private int InputPinValue;                      // value being fed from the Arduino
    private int ButtonPressValue;                   // value of InputPin when button is pressed
    private int ReleasePressValue;                  // value of InputPin when button is depressed
    private int InteractionType = 0;                // the int that determines the case for the main switchcase
    private int ShellState = 0;                     // the int that determines the case for the shell switchcase
    private int _lastActiveProgram;                 // the int value for the last active program in the active program list (count - 1)

    private float ReachDistance = 1000.0f;           // how far the raycast for grabbing reaches
    private float SmoothVelocity = 1.0f;            // part of the highlight effect
    private float HoverScaleDynamic = 1.0f;         // determines the mathf.smoothdamp for the highlight effect
    private float LerpTime = 15.0f;                 // part of the Lerp logic
    private float CurrentLerpTime = 0.0f;           // part of the Lerp logic

    private GameObject TargetLocation;              // the object that is lerped to when grabbing
    private GameObject ClonedObject;                // this will be instantiated to create the highlight effect                            
    private GameObject SelectedObject;               // the object being hit by the raycast
    private GameObject LeapMotionRig;               // this is the Leap Motion Rig
    private GameObject TeleportTarget;              // this is a dynamic object that you teleport to
    private GameObject EgocentricOrigin;            // the main object that is rubber banded to you
    private GameObject GazeController;              // the object the input controller will follow when gaze modality is active
    private GameObject ManualController;            // the object the input controller will follow when manual interaction is active
    private GameObject ContextualShell;             // the contextual shell master
    private GameObject SelectedObjectProxy;         // will move to the center of selected objects
    private GameObject[] ShellParents;              // an array of objects that will be activated on a trigger
    private List<GameObject> ActivePrograms;            // an array of objects that will be activated on a trigger

    private LineRenderer contextualLineRenderer;    // the line render drawn from the active program to the shell
    private LineRenderer RaycastLineRender;         // the linerenderer that shows where the user will act

    private ObjectMass ObjectMassScript;            // the script that determines the responsiveness of grabbed objects

    [HideInInspector]
    public bool LerpState = false;                  // determines whether to initialise the grabbing Lerp
    private bool ShellActive = false;               // TEMP
    private bool Mode;
    [HideInInspector]
    public bool ButtonPress = false;                // this is reffered to from other scripts to trigger events
    [HideInInspector]
    public bool IndirectSelectionState = true;      // this is reffered to from other scripts to trigger selection
    private bool _gazeActive;                       // this will be true when the right hand is inactive
    private bool _manualActive;                     // this will be true when the right hand is active
    private bool _lineRenderTrue;                   // dynamic bool for the linerenderer state
    private bool _lineRenderFalse;                  // dynamic bool for the linerenderer state
    private bool _shellActive;                      // ia the contextual shell active?
    #endregion
    #region Inspector and Public Variables
    [Header("Grab Settings")]
    [Space(5)]
    public int InputPin = 2;                        // what pin on the Arduino is the button connected to
    public bool IsInputInverted = false;            // change this based on the wiring of the button
    public bool LineRendererEnabled;        // enable line renderer?

    [Header("Grab Physics")]
    [Space(5)]
    public float DefaultObjectMass = 5.0f;          // the responsiveness of a grabbed object

    [Header("Gaze Cursor Settings")]
    [Space(5)]
    public bool GazeCursorEnabled;                  // enable the gaze cursor?
    public GameObject GazeCursor;                   // the parent object which will handle the gaze cursor states

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
        GazeController = GameObject.Find("Gaze Input Controller");
        ManualController = GameObject.Find("Manual Input Controller");
        ContextualShell = GameObject.Find("Contextual Shell ----- | Parent");
        RaycastLineRender = GetComponent<LineRenderer>();
        contextualLineRenderer = ContextualShell.GetComponent<LineRenderer>();
        ActivePrograms = new List<GameObject>();
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
        _lineRenderTrue = LineRendererEnabled ? true : false;
        _lineRenderFalse = LineRendererEnabled ? false : false;
        ButtonPress = (InputPinValue == ButtonPressValue) ? ButtonPress = true : ButtonPress = false;
        InteractionType = 0; 
        Ray GrabRay = new Ray(transform.position, transform.forward);
        RaycastHit HitPoint;
        InputPinValue = arduino.digitalRead(InputPin);
        if (Physics.Raycast(GrabRay, out HitPoint, ReachDistance, LayerMask.NameToLayer("IgnoreIndirectGrab")))
        {
            SelectedObject = HitPoint.transform.gameObject;
            ActivePrograms.Add(SelectedObject);
            _lastActiveProgram = ActivePrograms.Count - 1;
            #region Indirect Grab       | 1
            if (HitPoint.transform.tag == "GrabObject")
            {
                if (SelectedObject.GetComponent<ProgramLogic>() != null)
                {
                    SelectedObject.GetComponent<ProgramLogic>().Invoke("OnHoverStart", 0);
                }
                InteractionType = 1;
                RaycastLineRender.enabled = _lineRenderTrue;
                RaycastLineRender.useWorldSpace = true;
                Vector3 StartPoint = transform.position;
                Vector3 EndPoint = SelectedObject.transform.position;
                RaycastLineRender.SetPosition(0, StartPoint);
                RaycastLineRender.SetPosition(1, EndPoint);
            }
            #endregion
            #region Teleportation       | 2
            if (HitPoint.transform.tag == "TeleportLocation" && TeleportEnabled == true)
            {
                InteractionType = 2;
                RaycastLineRender.enabled = _lineRenderTrue;
                RaycastLineRender.useWorldSpace = true;
                Vector3 StartPoint = transform.position;
                Vector3 EndPoint = SelectedObject.transform.position;
                RaycastLineRender.SetPosition(0, StartPoint);
                RaycastLineRender.SetPosition(1, EndPoint);
                SelectedObject.GetComponent<BlendshapeAnimation>().OnTriggerStart();
            }
            #endregion
            #region Selection           | 3
            if (HitPoint.transform.tag == "SelectableUI" && IndirectSelectionEnabled == true)
            {
                InteractionType = 3;
                RaycastLineRender.enabled = _lineRenderTrue;
                RaycastLineRender.useWorldSpace = true;
                Vector3 StartPoint = transform.position;
                Vector3 EndPoint = SelectedObject.transform.position;
                RaycastLineRender.SetPosition(0, StartPoint);
                RaycastLineRender.SetPosition(1, EndPoint);
            }
            #endregion
        }
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
                        RaycastLineRender.enabled = _lineRenderFalse;
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
            default:
                #region Default;
                RaycastLineRender.enabled = _lineRenderFalse;
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
                if (ActivePrograms[_lastActiveProgram].transform.gameObject.GetComponent<ProgramLogic>() != null)
                {
                    ActivePrograms[_lastActiveProgram].transform.gameObject.GetComponent<ProgramLogic>().Invoke("OnHoverEnd", 0);
                }
                break;
                #endregion
        }
        #region Gaze Cursor
        if (GazeCursorEnabled)
        {
            GazeCursor.transform.position = HitPoint.point;
        }
        #endregion
        #region Input Controller
        if (_manualActive == true && ManualController != null)
        {
            LineRendererEnabled = true;
            GazeCursorEnabled = false;
            transform.position = ManualController.transform.position;
            transform.localRotation = ManualController.transform.localRotation;
        }
        if (_gazeActive == true && GazeController != null)
        {
            LineRendererEnabled = false;
            GazeCursorEnabled = true;
            transform.position = GazeController.transform.position;
            transform.eulerAngles = GazeController.transform.eulerAngles;
        }
        #endregion
        #region Contextual Shell
        if (_shellActive == true)
        {
            if (ActivePrograms.Count > 0 && ActivePrograms[_lastActiveProgram].transform.gameObject.GetComponent<ProgramLogic>() != null)
            {
                ActivePrograms[_lastActiveProgram].transform.gameObject.GetComponent<ProgramLogic>().Invoke("OnShellOpen", 0);
            }
            contextualLineRenderer.SetPosition(0, ContextualShell.transform.position);
            contextualLineRenderer.SetPosition(1, ActivePrograms[_lastActiveProgram].transform.position);
        }
        if (_shellActive == false)
        {   // some funky shit with the ArgumentOutOfRangeException...
            /*
            if (ActivePrograms.Count > 0 && ActivePrograms[_lastActiveProgram].transform.gameObject.GetComponent<ProgramLogic>() != null)
            {
                ActivePrograms[_lastActiveProgram].transform.gameObject.GetComponent<ProgramLogic>().Invoke("OnShellClose", 0);
            }*/
        }
        #endregion
    }
    void LateUpdate()
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
    #region Modality Methods
    public void ManualInput()
    {
        _manualActive = true;
        _gazeActive = false;
    }
    public void GazeInput()
    {
        _manualActive = false;
        _gazeActive = true;
    }
    #endregion
    #region Contextual Shell Methods
    public void ContextualShellLineRenderActive()
    {
        _shellActive = true;
        contextualLineRenderer.enabled = true;
    }
    public void ContextualShellLineRenderDeactive()
    {
        _shellActive = false;
        contextualLineRenderer.enabled = false;
    }
    #endregion
    #region Active Program Methods
    public void LaunchActiveProgram()
    {
        ActivePrograms[_lastActiveProgram].transform.gameObject.GetComponent<ProgramLogic>().Invoke("OnLaunch", 0);
    }
    public void CloseActiveProgram()
    {
        ActivePrograms[_lastActiveProgram].transform.gameObject.GetComponent<ProgramLogic>().Invoke("OnClose", 0);
    }
    #endregion
}