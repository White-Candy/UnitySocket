using CandySocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWindow : BaseUI
{
    public GameObject temp_item;

    void Start()
    {
        UIController.winDic.Add(UIState.US_Main, this);
    }

    void Update()
    {
        
    }
}
