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

[Serializable]
public class RetureClientBody
{
    public string state;
    public string body;
}

public class Launcher : MonoBehaviour
{
    void Start()
    {
        ServerContorller.Instance.StartListen(4530);
        RetureClientBody body = new RetureClientBody();
        UserTable user = new UserTable();
        user.user = "userr";
        user.scores = 0;
        user.id = 0;
        user.password = "password";

        body.state = "success";
        body.body = JsonMapper.ToJson(user);
        string json = JsonMapper.ToJson(body);
        Debug.Log(json);


        JsonData data = JsonMapper.ToObject(json);
        Debug.Log(data["state"]);
        string b = data["body"].ToString();
        JsonData data1 = JsonMapper.ToObject(b);
        Debug.Log(data1["id"]);
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
