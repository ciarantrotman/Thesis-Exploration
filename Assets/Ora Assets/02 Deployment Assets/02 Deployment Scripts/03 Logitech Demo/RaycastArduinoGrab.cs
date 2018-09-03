using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class RaycastArduinoGrab : MonoBehaviour
{
    private Arduino arduino;

    [Header("Grab Settings")]
    public float ReachDistance = 100.0f;
    public int InputPin = 2;
    private int InputPinValue = 1;
    private GameObject InventoryObject;

    [Header("Highlight Settings")]
    [Space(5)]
    public bool HighlightOn = false;
    [Space(10)]
    public float HoverScale = 1.1f;
    public float ScaleUpTime = .25f;
    public float ScaleDownTime = .75f;
    public Material HighlightMaterial;

    private GameObject ClonedObject;
    private float SmoothVelocity = 1.0f;
    private float HoverScaleDynamic = 1.0f;
    private bool GrabbingObject = false;
    private GameObject HitObject;
    private GameObject GrabbedObject;
    private int InventoryCount;

    void Start()
    { 
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
        InventoryObject = gameObject;
    }

    void ConfigurePins()
    {
        arduino.pinMode(InputPin, PinMode.INPUT);
        arduino.reportDigital((byte)(InputPin / 8), 1);
    }

  void Update()
    {
        Ray GrabRay = new Ray(transform.position, transform.forward);
        RaycastHit HitPoint;

        InputPinValue = arduino.digitalRead(InputPin);

        InventoryCount = InventoryObject.transform.childCount;

        if (Physics.Raycast(GrabRay, out HitPoint, ReachDistance) && HitPoint.transform.tag == "GrabObject")
        {
            GameObject HoverObject = HitPoint.transform.gameObject;
            GetComponent<LineRenderer>().enabled = true;

            if (ClonedObject == null && HighlightOn == true)
            {
                ClonedObject = Instantiate(HitPoint.transform.gameObject, HitPoint.transform.position, HitPoint.transform.rotation);
                ClonedObject.GetComponent<Collider>().enabled = false;
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

            if (ClonedObject != null && HighlightOn == true)
            {
                HoverScaleDynamic = (Mathf.SmoothDamp(HoverScaleDynamic, HoverScale, ref SmoothVelocity, ScaleUpTime));
            }

            if (InputPinValue == 0)
            {
                if (InventoryCount == 0)
                {
                    HoverObject.transform.parent = InventoryObject.transform;
                    GetComponent<LineRenderer>().enabled = false;
                }

                if (ClonedObject != null && HighlightOn == true)
                {
                    HoverScaleDynamic = (Mathf.SmoothDamp(HoverScaleDynamic, .95f, ref SmoothVelocity, .1f));
                }

                if (HoverScaleDynamic < 1 && HighlightOn == true)
                {
                    Destroy(ClonedObject);
                }
            }

            if (InputPinValue == 1)
            {
                HitPoint.transform.parent = null;
                GetComponent<LineRenderer>().enabled = true;
                HoverObject = null;
            }
        }

        else
        {
            GetComponent<LineRenderer>().enabled = false;

            if (ClonedObject != null && HighlightOn == true)
            {
                HoverScaleDynamic = (Mathf.SmoothDamp(HoverScaleDynamic, .95f, ref SmoothVelocity, ScaleDownTime));
            }

            if (HoverScaleDynamic < 1 && HighlightOn == true)
            {
                Destroy(ClonedObject);
            }
        }
    }

    private void LateUpdate()
    {
        if (ClonedObject != null && HighlightOn == true)
        {
            ClonedObject.transform.localScale = new Vector3 (HoverScaleDynamic, HoverScaleDynamic, HoverScaleDynamic);
            ClonedObject.transform.localEulerAngles = new Vector3 (0, 0, 0);
        }
    }
}
