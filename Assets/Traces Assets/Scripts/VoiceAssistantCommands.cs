using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VoiceAssistantCommands : MonoBehaviour
{
	public void VoiceCommand(string command, UnityEvent commandEvent)
	{
		if (command == GameObject.Find("VoiceAssistantRef").GetComponent<Text>().ToString())
		{
			commandEvent.Invoke();
		}	
	}
}