using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DijkstraFill))]
public class DijkstraFill_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("FILL"))
        {
            (target as DijkstraFill)?.StartFill();
        }
    }
}
