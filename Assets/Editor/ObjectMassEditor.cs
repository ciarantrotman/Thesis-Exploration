using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObjectMass))]

public class ObjectMassEditor : Editor
{

    public override void OnInspectorGUI()
    {
        ObjectMass TargetScript = (ObjectMass)target;
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("Define the mass of this object, the higher the value, the more sluggish it will be when you grab it.", MessageType.None, true);
        TargetScript.DigitalObjectMass = EditorGUILayout.Slider("Object Mass:", TargetScript.DigitalObjectMass, 0, 100);
        if (TargetScript.DigitalObjectMass < 3)
        {
            EditorGUILayout.HelpBox("If you make the mass " + TargetScript.DigitalObjectMass + ", it will be very jittery, consider making this higher.", MessageType.Warning, true);
        }

        if (TargetScript.DigitalObjectMass > 20)
        {
            EditorGUILayout.HelpBox("If you make the mass " + TargetScript.DigitalObjectMass + ", it will be very sluggish, consider making this lower.", MessageType.Warning, true);
        }
    }
}