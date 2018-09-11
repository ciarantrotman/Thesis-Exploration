using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using UnityEngine;

public class DirectionalMenu : MonoBehaviour
{
    private GameObject _ctrlNode;
    private GameObject _selectedNode;
    private GameObject _thumbTip;
    [HideInInspector]
    public List<GameObject> MenuNodes;
    private float _ctrlDistance;
    private int _graspEndSwitch;
    private bool _menuActive;
    
    public float CancelDistance;
    public float TriggerDistance;
    
    private void Start()
    {
        _ctrlNode = GameObject.Find("DirMenCtrlNode");
    }

    private void Update()
    {
        _thumbTip = GameObject.FindGameObjectWithTag("RightHandFingerController");
        Invoke(_thumbTip.GetComponent<FingerTriggerController>().Touching == true ? "MenuActive" : "MenuSummonEnd", 0);
    }

    public void MenuSummonBegin()
    {
        _menuActive = true;
        transform.GetComponent<LineRenderer>().enabled = true;
        transform.position = GameObject.Find("DirMenuSummon").transform.position;
        transform.LookAwayFrom(GameObject.Find("HMD Camera").transform);
    }

    public void MenuActive()
    {
        if (_menuActive == false) return;
        _graspEndSwitch = 0;
        
        //GameObject.Find("ThumbTip").GetComponent<FingerTriggerController>().HoldTimerUi.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);

        _thumbTip.GetComponent<FingerTriggerController>().HoldTimerUi.GetComponent<LineRendererCurved>().EndAngle = 0;
        
        _ctrlDistance = Vector3.Magnitude(transform.position - GameObject.Find("DirMenuSummon").transform.position);
        foreach (var menuNode in MenuNodes)
        {
            menuNode.GetComponent<NodeBehaviour>().CtrlNodeDistance = Vector3.Magnitude(_ctrlNode.transform.position - menuNode.transform.position);
        }
        MenuNodes.Sort(SortByDistance);
        _selectedNode = MenuNodes[0].gameObject;
        
        _ctrlNode.transform.position = GameObject.Find("IndexTip").transform.position;
        transform.GetComponent<LineRenderer>().SetPosition(0, GameObject.Find("DirMenuSummon").transform.position);
        transform.GetComponent<LineRenderer>().SetPosition(1,
            _ctrlDistance > TriggerDistance || _ctrlDistance > CancelDistance
                ? _selectedNode.transform.position
                : GameObject.Find("DirMenuSummon").transform.position);
    }
    
    public void MenuSummonEnd()
    {
        _menuActive = false;
        transform.GetComponent<LineRenderer>().enabled = false;
        transform.position = new Vector3(0,0,0);
        
        if (_ctrlDistance < TriggerDistance || _ctrlDistance > CancelDistance || _graspEndSwitch != 0) return;
        _selectedNode.GetComponent<NodeBehaviour>().Invoke("NodeTriggered", 0);
        _graspEndSwitch++;
    }

    private static int SortByDistance(GameObject obj1, GameObject obj2)
    {
        return obj1.GetComponent<NodeBehaviour>().CtrlNodeDistance.CompareTo(obj2.GetComponent<NodeBehaviour>().CtrlNodeDistance);
    }
}
