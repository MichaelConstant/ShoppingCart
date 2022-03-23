using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PhotonRoomSystem))]
public class PhotonRoomSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var photonRoomSystem = (PhotonRoomSystem) target;

        if (GUILayout.Button("Join Random Room"))
        {
            photonRoomSystem.JoinRandomRoom();
        }

        if (GUILayout.Button("Test"))
        {
            photonRoomSystem.OnTestButtonClicked();
        }
    }
}