using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using CandySocket;
using System.Net.Sockets;
using LitJson;

public class Launcher : MonoBehaviour
{
    void Start()
    {
        ServerContorller.Instance.StartListen(4530);
        //JsonData data = new JsonData();
        //string json = JsonMapper.ToJson(DatabaseController.Kinder.users);
        //data["body"] = json;
        ////Debug.Log(data.ToJson());

        //string list = data["body"]?.ToString();
        //List<UserTable> users = new List<UserTable>();
        //users = JsonMapper.ToObject<List<UserTable>>(list);

        //foreach(var  user in users )
        //{
        //    Debug.Log(user.id + " : " + user.name);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalParam.MessageQueue.Count > 0)
        {
            var pkg = GlobalParam.MessageQueue.Dequeue();
            Socket cli = pkg.cli;
            string ret = pkg.ret;

            GlobalParam.uimessage.AdditionalContent(ret);
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
        //ServerContorller.Instance.Clear();
    }

    void OnApplicationQuit()
    {
        ServerContorller.Instance.Clear();
    }
}
