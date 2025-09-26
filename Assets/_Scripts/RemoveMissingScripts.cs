using UnityEngine;
using UnityEditor;

public class RemoveMissingScripts : MonoBehaviour
{
    [MenuItem("Tools/Cleanup/Remove Missing Scripts In Selection")]
    static void RemoveMissingInSelection()
    {
        int count = 0;
        foreach (GameObject go in Selection.gameObjects)
        {
            count += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
        }
        Debug.Log($" {count} Missing Script ");
    }

    [MenuItem("Tools/Cleanup/Remove Missing Scripts In Scene")]
    static void RemoveMissingInScene()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true);
        int count = 0;
        foreach (GameObject go in allObjects)
        {
            count += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
        }
        Debug.Log($" {count} Missing Script ");
    }
}
