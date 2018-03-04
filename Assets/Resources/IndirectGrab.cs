using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Uniduino;

public class IndirectGrab : MonoBehaviour
{
    #region Inspector
    private Arduino arduino;

    [Header("Grab Settings")]
    [Space(5)]
    public GameObject TargetLocation;
    public int InputPin = 2;
    public bool IsInputInverted = false;
    private int ButtonPressValue;
    private int ReleasePressValue;
    private float ReachDistance = 100.0f;
    private int InputPinValue;
    private GameObject InventoryObject;

    [Header("Grab Physics")]
    [Space(5)]
    public float DefaultObjectMass = 10.0f;

    [Header("Highlight Settings")]
    [Space(5)]
    [Tooltip("This enables or disables the highlight visual feedback feature.")]
    public bool HighlightEnabled = true;
    [Space(5)]
    public float HoverScale = 1.1f;
    [Space(3)]
    public float ScaleUpTime = .15f;
    public float ScaleDownTime = .75f;
    [Space(3)]
    public Material HighlightMaterial;

    [Header("Teleport Settings")]
    [Space(5)]
    public bool TeleportEnabled = true;

    [Header("Indirect Selection Settings")]
    [Space(5)]
    public bool IndirectSelectionEnabled = true;
    public bool IndirectSelectionState = true;

    private GameObject ClonedObject;
    private float SmoothVelocity = 1.0f;
    private float HoverScaleDynamic = 1.0f;
    private bool GrabbingObject = false;
    private GameObject HitObject;
    private GameObject GrabbedObject;
    private int InventoryCount;

    [Header("Shell Settings")]
    [Space(5)]
    public bool ShellEnabled = true;
    private bool ShellActive = false;
    private bool ButtonPress = false;
    private int ShellState = 0;
    private bool ShellTrigger = false;
    public GameObject ShellParent;

    private float LerpTime = 15.0f;
    private GameObject SelectedObject;
    private GameObject PlayerLocation;
    private GameObject TeleportTarget;
    private float StartTime;
    private float JourneyLength;
    private float CurrentLerpTime = 0.0f;
    private bool LerpState = false;
    private ObjectMass ObjectMassScript;
    private int InteractionType = 0;

    private GameObject EgocentricParent;
    #endregion

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
        InventoryObject = gameObject;
        //TargetLocation = GameObject.Find("Target Location");
        PlayerLocation = GameObject.Find("Leap Motion Rig");
        TeleportTarget = GameObject.Find("Teleport Target");
        EgocentricParent = GameObject.Find("Egocentric Content Origin");
        if (IsInputInverted == false)
        {
            ButtonPressValue = 0;
            ReleasePressValue = 1;
        }
        if (IsInputInverted == true)
        {
            ButtonPressValue = 1;
            ReleasePressValue = 0;
        }
    }

    void ConfigurePins()
    {
        arduino.pinMode(InputPin, PinMode.INPUT);
        arduino.reportDigital((byte)(InputPin / 8), 1);
    }

    void Update()
    {
        InteractionType = 0;

        Ray GrabRay = new Ray(transform.position, transform.forward);
        RaycastHit HitPoint;

        InputPinValue = arduino.digitalRead(InputPin);
        InventoryCount = InventoryObject.transform.childCount;
        if (InputPinValue == ButtonPressValue)
        {
            ButtonPress = true;
        }
        if (InputPinValue == ReleasePressValue)
        {
            ButtonPress = false;
        }
        #region Indirect Grab
        if (Physics.Raycast(GrabRay, out HitPoint, ReachDistance) && HitPoint.transform.tag == "GrabObject")
        {
            InteractionType = 1;
            GetComponent<LineRenderer>().enabled = true;
            SelectedObject = HitPoint.transform.gameObject;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Invoke("EgoToExo", 0);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Invoke("ExoToEgo", 0);
            }
        }
        #endregion
        #region Teleportation
        if (Physics.Raycast(GrabRay, out HitPoint, ReachDistance) && HitPoint.transform.tag == "TeleportLocation")
        {
            if (TeleportEnabled == true)
            {
                InteractionType = 2;
                GetComponent<LineRenderer>().enabled = true;

                SelectedObject = HitPoint.transform.gameObject;
                SelectedObject.GetComponent<BlendshapeAnimation>().OnTriggerStart();
            }
        }
        #endregion
        #region Selection
        if (Physics.Raycast(GrabRay, out HitPoint, ReachDistance) && HitPoint.transform.tag == "SelectableUI")
        {
            if (IndirectSelectionEnabled == true)
            {
                InteractionType = 3;
                GetComponent<LineRenderer>().enabled = true;
            }
        }
        #endregion
        #region Contextual Menu
        if (Physics.Raycast(GrabRay, out HitPoint, ReachDistance) && HitPoint.transform.tag == "CONTEXTUAL MENU I GUESS")
        {
            InteractionType = 4;
        }
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
                if (InputPinValue == ButtonPressValue)
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

                        GetComponent<LineRenderer>().enabled = false;
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
                if (InputPinValue == ReleasePressValue)
                {
                    GetComponent<LineRenderer>().enabled = true;
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
                Vector3 TeleportPosition = PlayerLocation.transform.position;
                Vector3 TeleportTargetPosition = TeleportTarget.transform.position;
                if (InputPinValue == ButtonPressValue)
                {
                    TeleportPosition.x = TeleportTargetPosition.x;
                    TeleportPosition.z = TeleportTargetPosition.z;
                    PlayerLocation.transform.position = TeleportPosition;
                }
                break;
                #endregion
            case 3:
                #region Selection
                if (InputPinValue == ButtonPressValue)
                {
                    IndirectSelectionState ^= true;   
                }
                break;
                #endregion
            case 4:
                break;
            case 5:
                break;
            default:
                #region Default
                GetComponent<LineRenderer>().enabled = false;
                if (ClonedObject != null && HighlightEnabled == true)
                {
                    HoverScaleDynamic = (Mathf.SmoothDamp(HoverScaleDynamic, .95f, ref SmoothVelocity, ScaleDownTime));
                }
                if (HoverScaleDynamic < 1 && HighlightEnabled == true)
                {
                    Destroy(ClonedObject);
                }
                if (SelectedObject != null)
                {
                    SelectedObject.GetComponent<BlendshapeAnimation>().OnTriggerEnd();
                }
                break;
                #endregion
        }
        Debug.Log(InputPinValue);
    }

    private void LateUpdate()
    {
        if (ShellEnabled == true && InteractionType == 0)
        {
            ShellTrigger = true;
        }
        else if (ShellEnabled == false || InteractionType != 0)
        {
            ShellTrigger = false;
        }

        if (ShellTrigger == true && ButtonPress == true && ShellParent.activeSelf == false)
        {
            ShellState = 1;
        }
        if (ShellTrigger == true && ButtonPress == true && ShellParent.activeSelf == true)
        {
            ShellState = 2;
        }

        switch (ShellState)
        {
            case 1:
                Debug.Log("SHELL LAUNCH: " + ButtonPressValue);
                Invoke("ShellLaunch", 0);
                StartCoroutine("ShellDelay");
                break;
            case 2:
                Debug.Log("SHELL CLOSE: " + ButtonPressValue);
                Invoke("ShellClose", 0);
                break;
            default:
                break;
        }
        if (ClonedObject != null && HighlightEnabled == true)
        {
            ClonedObject.transform.localScale = new Vector3(HoverScaleDynamic, HoverScaleDynamic, HoverScaleDynamic);
            ClonedObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (LerpState == true)
        {
            CurrentLerpTime += Time.deltaTime;

            if (CurrentLerpTime >= 1)
            {
                CurrentLerpTime = 0;
            }

            float JourneyPercentage = CurrentLerpTime / LerpTime;

            SelectedObject.transform.position = Vector3.Lerp(SelectedObject.transform.position, TargetLocation.transform.position, JourneyPercentage);
        }
    }

    #region Content Locking Logic
    public void ExoToEgo()
    {
        Debug.Log(SelectedObject.transform.name);
        if (EgocentricParent != null && SelectedObject != null)
        {
            SelectedObject.transform.parent = EgocentricParent.transform;
        }
    }
    public void EgoToExo()
    {
        Debug.Log("NEET");
        if (EgocentricParent != null && SelectedObject != null)
        {
            SelectedObject.transform.parent = null;
        }
    }
    #endregion

    #region Shell Logic
    public void ShellLaunch()
    {
        Debug.Log("SHELL ACTIVATION");
        ShellParent.SetActive(true);
    }
    public void ShellClose()
    {
        Debug.Log("SHELL DEACTIVATION");
        ShellParent.SetActive(false);
    }
    private IEnumerator ShellDelay()
    {
        ShellTrigger = false;
        yield return new WaitForSeconds(1);
    }
    #endregion
}
