using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Curve), true)]
public class CurveEditor : Editor
{
    private Curve curve;

    private void OnEnable()
    {
        curve = target as Curve;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Compute"))
            curve.Compute();
    }
}
