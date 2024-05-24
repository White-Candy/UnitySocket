using CandySocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Config.init();
        ClientController.Instance.Init(Config.url, 4530);

        GlobalParameterManager.TemplateWindow = GameObject.Find("Canvas/EditorCanvas").GetComponent<TemplateWindow>();
        GlobalParameterManager.MainWindow = GameObject.Find("Canvas/MainCanvas").GetComponent<MainWindow>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalParameterManager.MessQueue.Count > 0)
        {
            string ret = GlobalParameterManager.MessQueue.Dequeue();
            ReciveServerBody body = JsonController.Instance.StringToJson(ret);
            if (body != null)
            {
                IEvent spawn = EventSpawn.CreateEvent(body.type);
                spawn.OnEvent(body.body);
            }
        }
    }

    void OnApplicationQuit()
    {
        ClientController.Instance.SocketClose();
    }
}
