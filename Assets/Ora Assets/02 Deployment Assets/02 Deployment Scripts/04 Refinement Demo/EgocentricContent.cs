using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgocentricContent : MonoBehaviour {

    [Header("Egocentric Content Behaviour Settings")]
    [Space(5)]
    public float RubberbandSpeed;
    public float DistanceThreshold;
    public float RotationThreshold;

    private GameObject EgocentricContentOrigin;
    private GameObject UserCenter;
        
    private float CurrentLerpTime = 0.0f;
    private float JourneyPercentage;
    private float ContentDistance;
    private float ContentRotation;
    private bool LerpState = false;

    private float EgocentricRotationY;
    private float UserCenterY;

    void Start ()
    {
        UserCenter = GameObject.Find("User Center");
        EgocentricContentOrigin = gameObject;
    }

    void Update()
    {
        ContentDistance = Vector3.Distance(EgocentricContentOrigin.transform.position, UserCenter.transform.position);
        EgocentricRotationY = EgocentricContentOrigin.transform.eulerAngles.y;
        UserCenterY = UserCenter.transform.eulerAngles.y;
        ContentRotation = Mathf.DeltaAngle(EgocentricRotationY,UserCenterY);
        ContentRotation = Mathf.Abs(ContentRotation);
        if (ContentDistance > DistanceThreshold || ContentRotation > RotationThreshold)
        {
            LerpState = true;
        }
    }

    void LateUpdate ()
    {
        if (LerpState == true)
        {
            CurrentLerpTime += Time.deltaTime;
            if (CurrentLerpTime >= 1)
            {
                LerpState = false;
                CurrentLerpTime = 0;
            }
            JourneyPercentage = CurrentLerpTime / RubberbandSpeed;
            EgocentricContentOrigin.transform.position = Vector3.Lerp(EgocentricContentOrigin.transform.position, UserCenter.transform.position, JourneyPercentage);
            EgocentricContentOrigin.transform.rotation = Quaternion.Lerp(EgocentricContentOrigin.transform.rotation, UserCenter.transform.rotation, JourneyPercentage);
        }
    }
}
