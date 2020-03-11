using UnityEngine;
using UnityEditor;

public class Example
{
    [MenuItem("Examples/Instantiate Selected")]
    static void InstantiatePrefab()
    {
        Selection.activeObject = PrefabUtility.InstantiatePrefab(Selection.activeObject as GameObject);
    }

    [MenuItem("Examples/Instantiate Selected", true)]
    static bool ValidateInstantiatePrefab()
    {
        GameObject go = Selection.activeObject as GameObject;
        
        if (go == null)
            return false;
        go.transform.position = new Vector3(1, 10, 1);
        return PrefabUtility.IsPartOfPrefabAsset(go);
    }
}