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
using LitJson;

public class Launcher : MonoBehaviour
{
    void Start()
    {
        ServerContorller.Instance.StartListen(4530);

        RetureClientBody body = new RetureClientBody();

        body.state = "Close";
        //body.body = JsonMapper.ToJson(null);
        string mess = JsonMapper.ToJson(body);
        Debug.Log(mess);

        JsonMapper.ToObject(mess);
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
