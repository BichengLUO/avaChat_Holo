using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Button))]
public class ButtonEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Button script = target as Button;
        if (GUILayout.Button("Click"))
        {
            script.OnSelect();
        }
    }
}
