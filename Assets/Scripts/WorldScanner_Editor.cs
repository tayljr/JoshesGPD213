using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldScanner))]
public class WorldScanner_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("UPDATE"))
        {
            (target as WorldScanner)?.ScanWorld();
        }
    }
}
