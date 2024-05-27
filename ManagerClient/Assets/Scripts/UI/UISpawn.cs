using CandySocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawn : MonoBehaviour
{
    void Start()
    {
        GlobalParameterManager.TemplateWindow = GameObject.Find("Canvas/EditorCanvas").GetComponent<TemplateWindow>();
        GlobalParameterManager.MainWindow = GameObject.Find("Canvas/MainCanvas").GetComponent<MainWindow>();
    }

    void Update()
    {
        UIController.OnGUI();
    }
}
