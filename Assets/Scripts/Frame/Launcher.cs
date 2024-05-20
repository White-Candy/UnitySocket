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
using UnityEngine.SocialPlatforms.Impl;

public class Launcher : MonoBehaviour
{
    void Start()
    {
        ServerContorller.Instance.StartListen(4530);
        GlobalParam.MessageQueue.Enqueue(new ClientPkg(null, "{\"type\":\"Logon\",\"body\":{\"ID\":0,\"name\":\"123\"," +
            "\"password\":\"123\", \"registry\":\"\",\"login\":\"\",\"score\":0,\"score2\":0,\"score3\":0}}"));
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalParam.MessageQueue.Count > 0)
        {
            var pkg = GlobalParam.MessageQueue.Dequeue();
            Socket cli = pkg.cli;
            string ret = pkg.ret;

            ReciveClientBody body = JsonController.Instance.StringToJson(ret);
            if (body != null)
            {
                IEvent spawn = EventSpawn.CreateEvent(body.type);
                spawn.CliRetInfoProcess(cli, body.body);
            }
        }
    }

    void OnDestroy()
    {
        ServerContorller.Instance.Clear();
    }    
}
