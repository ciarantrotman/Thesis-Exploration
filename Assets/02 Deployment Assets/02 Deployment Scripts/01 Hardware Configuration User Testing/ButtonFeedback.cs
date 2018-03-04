using UnityEngine;
using System.Collections;

public class ButtonFeedback : MonoBehaviour
{
    public GameObject FeedbackGameObject;
    public GameObject TargetDisappear;

    public void FeedbackActivation()
    {
        FeedbackGameObject.SetActive(true);
        TargetDisappear.SetActive(false);
    }

    public void FeedbackDeactivation()
    {
        FeedbackGameObject.SetActive(false);
        TargetDisappear.SetActive(true);
    }
}