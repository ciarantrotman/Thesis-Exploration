using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextDuplication : MonoBehaviour
{
	public Text RefText;
	private void Update ()
	{
		var self = GetComponent<TextMeshProUGUI>();
		self.text = RefText.ToString();
	}
}
