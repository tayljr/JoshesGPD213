using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldScanner))]
public class WorldScanner_Editor : Editor
{
    private Vector3Int gridPos = Vector3Int.zero;
    private Vector3 worldPos = Vector3.zero;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("UPDATE"))
        {
            (target as WorldScanner)?.ScanWorld();
        }

        gridPos = EditorGUILayout.Vector3IntField("Grid Pos:", gridPos);

        if (GUILayout.Button("Grid to World"))
        {
            worldPos = (Vector3)(target as WorldScanner)?.GridToWorld(gridPos);
        }
        
        worldPos = EditorGUILayout.Vector3Field("World Pos:", worldPos);

        if (GUILayout.Button("World to Grid"))
        {
            gridPos = (Vector3Int)(target as WorldScanner)?.WorldToGrid(worldPos);
        }
    }
}
