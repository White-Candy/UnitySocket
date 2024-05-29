using CandySocket;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorSaveObj : MonoBehaviour
{
    void OnDestroy()
    {
#if UNITY_EDITOR
        //EditorUtility.SetDirty(DatabaseController.Kinder);
        //AssetDatabase.SaveAssets();
#endif
        //ServerContorller.Instance.Clear();
    }

    void OnApplicationQuit()
    {
#if UNITY_EDITOR
        //EditorUtility.SetDirty(DatabaseController.Kinder);
        //AssetDatabase.SaveAssets();
#endif
        //ServerContorller.Instance.Clear();
    }
}
