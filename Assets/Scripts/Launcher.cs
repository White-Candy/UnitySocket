using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using CandySocket;
using System.Net.Sockets;
using LitJson;
using UnityEditor;
using System.IO;

public class Launcher : MonoBehaviour
{
    void Start()
    {
        ServerContorller.Instance.StartListen(4530);
        for (int i = 0; i < 10000; i++)
        {
            if (DatabaseController.Kinder.users.Exists(x => x.id == i))
            {
                continue;
            }
            DatabaseController.idList.Add(i);
        }
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
        //EditorUtility.SetDirty(DatabaseController.Kinder);
        //AssetDatabase.SaveAssets();
        //ServerContorller.Instance.Clear();
        //JsonUtility.FromJsonOverwrite(json, DatabaseController.Kinder);
        string json = JsonUtility.ToJson(DatabaseController.Kinder);
        File.WriteAllText(Application.persistentDataPath + "/Data.json", json);
    }

    void OnApplicationQuit()
    {
        //EditorUtility.SetDirty(DatabaseController.Kinder);
        //AssetDatabase.SaveAssets();
        string json = JsonUtility.ToJson(DatabaseController.Kinder);
        File.WriteAllText(Application.persistentDataPath + "/Data.json", json);
        ServerContorller.Instance.Clear();
    }
}
