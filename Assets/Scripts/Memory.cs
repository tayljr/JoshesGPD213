using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Memory : SerializedMonoBehaviour
{
    public Dictionary<GameObject, Vector3> memoryDic = new Dictionary<GameObject, Vector3>();
    public void NewMemory(Dictionary<GameObject, Vector3> newMemories)
    {
        foreach (KeyValuePair<GameObject, Vector3> newMem in newMemories)
        {
            if (memoryDic.ContainsKey(newMem.Key))
            {
                memoryDic.Remove(newMem.Key);
            }
            memoryDic.Add(newMem.Key, newMem.Value);
        }
    }
}
