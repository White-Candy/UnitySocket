using CandySocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpawn : MonoBehaviour
{
    public TemplateWindow Template;
    public MainWindow MainWindow;
    public MessageBox MessageBox;

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
