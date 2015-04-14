using UnityEditor;
using UnityEngine;

public static class SlotMenuBar
{
    [MenuItem("Markus Tools/Instantiate Selected")]
    static void CreatePrefab()
    {
        //var clone = PrefabUtility.InstantiatePrefab(Selection.activeObject as GameObject) as GameObject;
        var clone = Object.Instantiate(Selection.activeObject as GameObject) as GameObject;

        var root = GameObject.FindGameObjectWithTag("Root");
        if (root == null || clone == null)
        {
            return;
        }

        clone.transform.parent = root.transform;
        clone.transform.localRotation = Quaternion.identity;
        clone.transform.localScale = Vector3.one;
    }

    [MenuItem("Markus Tools/Instantiate Selected", true)]
    static bool ValidateCreatePrefab()
    {
        var go = Selection.activeObject as GameObject;
        if (go == null)
        {
            return false;
        }

        return PrefabUtility.GetPrefabType(go) == PrefabType.Prefab || PrefabUtility.GetPrefabType(go) == PrefabType.ModelPrefab;
    }
}
