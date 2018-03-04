using UnityEngine;
using System.Collections;
using Uniduino;

public class GrabObject : MonoBehaviour
{
    private Arduino arduino;
    public int InputPin = 2;
    public int InputPinValue = 1;
    bool grabbed = false;
    bool collided = false;

    public GameObject ChildObject;
    public GameObject ParentObject;
    public GameObject RaycastingObject;
    public GameObject LineRenderer;
    public GameObject DefaultParent;

    private int HitCount = 0;

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
    }

    void ConfigurePins()
    {
        arduino.pinMode(InputPin, PinMode.INPUT);
        arduino.pinMode(13, PinMode.OUTPUT);
        arduino.reportDigital((byte)(InputPin / 8), 1);
    }

    void OnTriggerStay (Collider TargetObject)
    {
        if (TargetObject.gameObject.name == RaycastingObject.transform.name)
        {
            collided = true;
            HitCount++;
            //Debug.Log("NO. OF COLLISIONS: " + HitCount);
        }
    }

    void Update()
    {
        InputPinValue = arduino.digitalRead(InputPin);

        if (InputPinValue == 0 && collided == true) //grab
        {
            ChildObject.transform.parent = ParentObject.transform;
            LineRenderer.SetActive(false);
            RaycastingObject.SetActive(false);

            BoxCollider GrabbedObjectBoxCollider = GetComponent<BoxCollider>();
            GrabbedObjectBoxCollider.isTrigger = false;
            /*
            Rigidbody GrabbedObjectRigidbody = GetComponent<Rigidbody>();
            GrabbedObjectRigidbody.isKinematic = false;
            GrabbedObjectRigidbody.useGravity = false;
            */
        }

        else if (InputPinValue == 1) //release
        {
            ChildObject.transform.parent = DefaultParent.transform;
            LineRenderer.SetActive(true);
            RaycastingObject.SetActive(true);
            collided = false;
            grabbed = false;
      
            BoxCollider GrabbedObjectBoxCollider = GetComponent<BoxCollider>();
            GrabbedObjectBoxCollider.isTrigger = true;
            /*
            Rigidbody GrabbedObjectRigidbody = GetComponent<Rigidbody>();
            GrabbedObjectRigidbody.isKinematic = true;
            GrabbedObjectRigidbody.useGravity = true;
            */
        }
    }
}


/*if (grabbed == false)
    {
        arduino.digitalWrite(13, Arduino.HIGH);
        yield return new WaitForSeconds(.25F);
        arduino.digitalWrite(13, Arduino.LOW);
        grabbed = true;
    }*/
