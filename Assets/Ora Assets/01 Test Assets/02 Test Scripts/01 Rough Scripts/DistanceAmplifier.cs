using UnityEngine;
using UnityEngine.Events;

public class DistanceAmplifier : MonoBehaviour
{

	public float ProxyDelta;
	public float IndirectDistance;
	public float AmplifiedDistance;

	public GameObject UserOrigin;
	public GameObject ReferenceProxy;
	public GameObject ControllerProxy;
	public GameObject IndirectObject;
	
	void Start () 
	{
		
	}
	
	void Update ()
	{
		ProxyDelta = (Vector3.Distance(ReferenceProxy.transform.position, ControllerProxy.transform.position));
		IndirectDistance = (Vector3.Distance(ReferenceProxy.transform.position, IndirectObject.transform.position));

		AmplifiedDistance = ProxyDelta*ProxyDelta;
		
		//Debug.Log("OD = " + ProxyDelta + " |  CD = " + IndirectDistance + " |  AD = " + AmplifiedDistance);
		
		IndirectObject.transform.position = new Vector3(IndirectObject.transform.position.x, IndirectObject.transform.position.y, AmplifiedDistance/*(IndirectObject.transform.position.z*AmplifiedDistance)*/);
	}
}
