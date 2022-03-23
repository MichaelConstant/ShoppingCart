using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoginSystem))]
public class LoginSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("Test Connecting", MessageType.Info);

        var loginSystem = (LoginSystem) target;
        if (GUILayout.Button("Test Connect"))
        {
            loginSystem.TestConnect();
        }
    }
}