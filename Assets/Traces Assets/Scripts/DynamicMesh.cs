using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMesh : MonoBehaviour
{
	private MeshFilter _mf;
	private Mesh _m;
	[Header("Vertex Positions")]
	[Tooltip("Ensure they are in the the correct order")]
	public List<GameObject> MeshVertices;
	
	private void Start ()
	{
		_mf = GetComponent<MeshFilter>();
		_m = _mf.mesh;
	}

	private void Update ()
	{
		_m.vertices = new Vector3[]
		{
			MeshVertices[0].transform.localPosition, 
			MeshVertices[1].transform.localPosition, 
			MeshVertices[2].transform.localPosition,
			MeshVertices[3].transform.localPosition,
		};
		_m.triangles = new int[]
		{
			0,2,3,
			3,1,0
		};
	}
}
