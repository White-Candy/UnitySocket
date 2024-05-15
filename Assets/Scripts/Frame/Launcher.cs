using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using CandySocket;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using UnityEditor.Experimental.UIElements.GraphView;

public class Launcher : MonoBehaviour
{
    void Start()
    {
        ServerContorller.Instance.StartListen(4530);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        ServerContorller.Instance.Clear();
    }    
}
