using CandySocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Config.init();
        ClientController.Instance.Init(Config.url, 4530);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
    {
        ClientController.Instance.SocketClose();
    }
}
