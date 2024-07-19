using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AStar))]
public class AStar_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("SCAN"))
        {
            (target as AStar)?.StartFill();
        }
        if (GUILayout.Button("STOP"))
        {
            (target as AStar)?.StopFill();
        }
    }
}