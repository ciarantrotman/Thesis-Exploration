using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VoiceAssistentTextToString : MonoBehaviour
{
	public TextMeshProUGUI DisplayText;
	public Text TextInput;

	private void Update ()
	{
		DisplayText.text = TextInput.text;
	}
}
