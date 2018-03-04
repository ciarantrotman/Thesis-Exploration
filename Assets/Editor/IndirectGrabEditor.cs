using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IndirectGrab))]

public class IndirectGrabEditor : Editor
{/*
    public override void OnInspectorGUI()
    {
        IndirectGrab TargetScript = (IndirectGrab)target;
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("This script controls the grabbing indirect action in the VR scene, there should be only one of these scripts in the scene, and it should be located where you want to grab from. You can change the settings and behaviours of this feature below.", MessageType.None, true);
        EditorGUILayout.Space();
        //EditorGUILayout.InspectorTitlebar(true, "What");
        TargetScript.ReachDistance = EditorGUILayout.Slider("Reach Distance:", TargetScript.ReachDistance, 10, 100);
        TargetScript.MovementSpeed = EditorGUILayout.Slider("Movement Speed:", TargetScript.MovementSpeed, 0, 50);
        TargetScript.InputPin = EditorGUILayout.IntField("Arduino Pin Number:", TargetScript.InputPin);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        TargetScript.HighlightOn = EditorGUILayout.Toggle("Highlight Enabled:", TargetScript.HighlightOn);
        if (TargetScript.HighlightOn == true)
        {
            EditorGUILayout.HelpBox("Here you can define the behaviour of the highlight border around a selected object:", MessageType.None, true);
            TargetScript.HoverScale = EditorGUILayout.FloatField("Highlight Border Size:", TargetScript.HoverScale);
            if (TargetScript.HoverScale > 2)
            {
                EditorGUILayout.HelpBox("If you make the border size " + TargetScript.HoverScale + ", it will be too big. Try to keep it below 2.", MessageType.Warning, true);
            }
            EditorGUILayout.BeginHorizontal();
            TargetScript.ScaleUpTime = EditorGUILayout.FloatField("Highlight Appear Duration:", TargetScript.ScaleUpTime);
            TargetScript.ScaleDownTime = EditorGUILayout.FloatField("Highlight Disappear Duration:", TargetScript.ScaleDownTime);
            EditorGUILayout.EndHorizontal();
            if (TargetScript.ScaleUpTime >= TargetScript.ScaleDownTime)
            {
                EditorGUILayout.HelpBox("Make sure that the visual effect you're going for here makes sense.", MessageType.Warning, true);
            }
        }
    }*/
}
