using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSelection : MonoBehaviour
{
    public float DirectDistance = 1.0f;
    
    public float MacroSelectionFov = 30f;
    public float MicroSelectionFov = 15f;
    private float _microSelectionFovRef;

    public Material LineRendererMaterial;
    
    [HideInInspector]
    public GameObject MicroSelectionOrigin;
    [HideInInspector]
    public GameObject ActiveObject;
    [HideInInspector]
    public GameObject LastActiveObject;
    private GameObject _self;
    
    public List<GameObject> SelectedObjects;
    [HideInInspector]
    public List<GameObject> SelectableObjects;
    [HideInInspector]
    public List<GameObject> GlobalSelectableObjects;
    
    private LineRenderer _lineRenderer;

    public UnityEvent MacroSelectBegin;
    public UnityEvent MacroSelectEnd;
    public UnityEvent MicroSelectBegin;
    public UnityEvent MicroSelectEnd;
 
    private void Start()
    {
        MicroSelectionOrigin = GameObject.Find("MicroSelectionOrigin");
        _self = transform.gameObject;
        LastActiveObject = null;
    }

    private void Update()
    {
        if (LastActiveObject != null)
            Debug.Log(LastActiveObject.transform.name);
        
        foreach (GameObject selectableObject in GlobalSelectableObjects)
        {
            float microAngle = selectableObject.GetComponent<ObjectBehaviours>().MicroAngle;
            float macroAngle = selectableObject.GetComponent<ObjectBehaviours>().MacroAngle;

            if (macroAngle < MacroSelectionFov / 2 && SelectableObjects.Contains(selectableObject) == false)
            {
                Invoke("OnMacroSelectBegin", 0);
                SelectableObjects.Add(selectableObject); 
            }

            if (macroAngle >= MacroSelectionFov / 2)
            {
                Invoke("OnMacroSelectEnd", 0);
                SelectableObjects.Remove(selectableObject);
                SelectedObjects.Remove(selectableObject);
            }

            if (microAngle < MicroSelectionFov / 2 && macroAngle < MacroSelectionFov / 2 && SelectedObjects.Contains(selectableObject) == false)
            {
                Invoke("OnMicroSelectBegin", 0);
                SelectedObjects.Add(selectableObject);
            }
            
            if (microAngle >= MicroSelectionFov / 2 || macroAngle >= MacroSelectionFov)
            {
                Invoke("OnMicroSelectEnd", 0);
                SelectedObjects.Remove(selectableObject);
            }
               
        }
        
        foreach (GameObject selectableObject in SelectableObjects)
        {
            Debug.DrawLine(transform.position, selectableObject.transform.position, Color.cyan);
        }
        
        foreach (GameObject activeObject in SelectedObjects)
        {
            Debug.DrawLine(activeObject.transform.position, MicroSelectionOrigin.transform.position, Color.blue);
        }
        
        SelectedObjects.Sort(SortByDeviation);

        ActiveObject = SelectedObjects.Count > 0 ? SelectedObjects[0].gameObject : null;
        LastActiveObject = ActiveObject == null ? LastActiveObject : ActiveObject;
        
        Invoke("DrawLineRenderer", 0);
    }

    #region Selection Events

    public void OnMacroSelectBegin()
    {
        
    }
    public void OnMacroSelectEnd()
    {
        
    }
    public void OnMicroSelectBegin()
    {
        
    }
    public void OnMicroSelectEnd()
    {

    }
    public void OnGrabBegin()
    {
        _microSelectionFovRef = MicroSelectionFov;
        MicroSelectionFov = 0;
    }
    public void OnGrabStay()
    {
        
    }
    public void OnGrabEnd()
    {
        MicroSelectionFov = _microSelectionFovRef;
    }

    #endregion

    private void DrawLineRenderer()
    {
        if (LastActiveObject == null) return;
        
        var midPointx = (LastActiveObject.transform.position.x - MicroSelectionOrigin.transform.position.x) / 2;
        var midPointy = (LastActiveObject.transform.position.y - MicroSelectionOrigin.transform.position.y) / 2;
        var midPointz = (LastActiveObject.transform.position.z - MicroSelectionOrigin.transform.position.z) / 2;
        var midPoint = new Vector3(midPointx, midPointy, midPointz);
        
        _lineRenderer = transform.GetComponent<LineRenderer>();
        if (_lineRenderer == null)
            _lineRenderer = _self.AddComponent<LineRenderer>();
        _lineRenderer.material = LineRendererMaterial;
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.SetWidth(0.0015f, 0.0015f);
        _lineRenderer.SetVertexCount(2);
        _lineRenderer.SetPosition(0, MicroSelectionOrigin.transform.position);
        //_lineRenderer.SetPosition(1, midPoint);
        _lineRenderer.SetPosition(1, ActiveObject != null ? ActiveObject.transform.position : MicroSelectionOrigin.transform.position);
    }
    
    public void ActiveProgramClear()
    {
        SelectedObjects.Clear();
    }
    
    int SortByDeviation(GameObject obj1, GameObject obj2)
    {
        return obj1.GetComponent<ObjectBehaviours>().MicroAngle.CompareTo(obj2.GetComponent<ObjectBehaviours>().MicroAngle);
    }
}
