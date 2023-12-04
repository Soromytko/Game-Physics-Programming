using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SolarSystem))]
public class SolarSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SolarSystem targetScriptObject = (SolarSystem)target;

        targetScriptObject.SimulationSpeed = EditorGUILayout.FloatField("SimulationSpeed", targetScriptObject.SimulationSpeed);


        EditorGUI.BeginChangeCheck();
        targetScriptObject.AstronomicalUnit = EditorGUILayout.FloatField("AstronomicalUnit", targetScriptObject.AstronomicalUnit);
        targetScriptObject.EarthRadius = EditorGUILayout.FloatField("EarthRadius", targetScriptObject.EarthRadius);
        if (EditorGUI.EndChangeCheck())
        {
            targetScriptObject.UpdatePlanets();
        }
        
        if (GUILayout.Button("Update Planets"))
        {
            targetScriptObject.UpdatePlanets();
        }

        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_planets"), true);
        serializedObject.ApplyModifiedProperties();

    }
}
