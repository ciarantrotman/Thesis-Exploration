using System.Collections;
using UnityEngine;
using Uniduino;

public class UniduinoBlinkLight : MonoBehaviour {

    public Arduino arduino;

	void Start () {
        arduino = Arduino.global;
        arduino.Setup(ConfigurePins);
        StartCoroutine(BlinkLoop());
	}

    void ConfigurePins() {
        arduino.pinMode(13, PinMode.OUTPUT);
    }

    IEnumerator BlinkLoop ()
    {
        while (true) {
            arduino.digitalWrite(13, Arduino.HIGH);
            yield return new WaitForSeconds(1);
            arduino.digitalWrite(13, Arduino.LOW);
            yield return new WaitForSeconds(1);
        }
    }

    void Update () {
		
	}
}
