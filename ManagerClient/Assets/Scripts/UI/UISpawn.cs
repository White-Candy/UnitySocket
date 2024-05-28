using CandySocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawn : MonoBehaviour
{
    public TemplateWindow Template;
    public MainWindow MainWindow;
    public MessageBox MessageBox;

    void Awake()
    {
        //if (!Debug.isDebugBuild)
        //{
        //    //WindowBar.HideWindowBar();
        //    //Screen.SetResolution(1280, 720, true);
        //}
    }

    void Start()
    {
        GlobalParameterManager.TemplateWindow = Template;
        GlobalParameterManager.MainWindow = MainWindow;
        GlobalParameterManager.MessageBox = MessageBox;
    }

    void Update()
    {
        UIController.OnGUI();
    }
}
