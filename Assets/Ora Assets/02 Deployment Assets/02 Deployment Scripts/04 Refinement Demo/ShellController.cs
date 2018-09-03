using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class ShellController : MonoBehaviour {

    [Header("Shell Settings")]
    [Space(5)]
    public bool ShellEnabled = true;
    public GameObject ShellParent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ShellParent.activeSelf == false)
        {
            Debug.Log("Active");
            ShellParent.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && ShellParent.activeSelf == true)
        {
            Debug.Log("Inactive");
            ShellParent.SetActive(false);
        }
    }
}

/*
    #region Arduino Settings
    private Arduino arduino;
    private int ButtonPressValue;
    private int ReleasePressValue;
    #endregion

    [Header("Arduino Setting")]
    [Space(5)]
    public int InputPin = 2;
    public bool IsInputInverted = false;
    public int InputPinValue = 1;

    [Header("Shell Settings")]
    [Space(5)]
    public bool ShellEnabled = true;
    public bool ButtonPress = false;
    public GameObject ShellParent;
    private bool ShellActive = false;

    void Start()
    {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
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

     InputPinValue = arduino.digitalRead(InputPin);
     if (InputPinValue == ButtonPressValue)
     {
         ButtonPress = true;
     }
     if (InputPinValue == ReleasePressValue)
     {
         ButtonPress = false;
     }
     
}

void LateUpdate()
{
    Debug.Log(ButtonPress);

    if (GameObject.Find("Manual Input Controller").activeSelf == true)
    {
        Debug.Log("SHOULD BE DOING NOTHING");
    }
    if (ButtonPress == true && ShellEnabled == true && GameObject.Find("Manual Input Controller").activeSelf == false)
    {
        if (ShellActive == false)
        {
            Debug.Log("SHELL LAUNCH");
            StartCoroutine("ShellLaunch");
        }
        if (ShellActive == true)
        {
            Debug.Log("SHELL CLOSE");
            StartCoroutine("ShellClose");
        }
    }

}
 private IEnumerator ShellLaunch()
    {
        Debug.Log("LAUNCH COROUTINE");
        if (ShellActive == false)
        {
            Debug.Log("SHELL ACTIVATION");
            ShellParent.SetActive(true);
            yield return new WaitForSeconds(2);
            ShellActive = true;
        }
    }
    private IEnumerator ShellClose()
    {
        Debug.Log("CLOSE COROUTINE");
        if (ShellActive == true)
        {
            Debug.Log("SHELL DEACTIVATION");
            ShellParent.SetActive(false);
            yield return new WaitForSeconds(2);
            ShellActive = false;
        }
    }
*/
