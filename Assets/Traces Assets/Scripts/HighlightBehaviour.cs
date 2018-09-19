using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBehaviour : MonoBehaviour
{
	public GameObject Target;
	public Material HighlightMaterial;
	public float HighlightScale;

	private GameObject _highlight;
	
	public void HighlightBegin()
	{
		_highlight = Instantiate(Target, transform.position, transform.rotation);
		_highlight.GetComponent<MeshRenderer>().material = HighlightMaterial;
		_highlight.transform.SetParent(transform);
		var origScale = Target.transform.localScale;
		_highlight.transform.localScale = origScale+(origScale*HighlightScale);
	}

	public void HighlightEnd()
	{
		Destroy(_highlight);
	}
}
