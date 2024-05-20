using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Net.Sockets;

namespace CandySocket
{
    public enum EventType
    {
        None,
        Close,
        Login, 
        Logon
    }

    public interface IEvent
    {
        void CliRetInfoProcess(Socket cli, JsonData body);
    }

    public class LoginEvent : IEvent
    {
        public void CliRetInfoProcess(Socket cli, JsonData body)
        {
            Debug.Log("LoginEvent");
            ServerContorller.Instance.SendToClient(cli, "LoginEventFinish!");
        }
    }

    public class LogonEvent : IEvent
    {
        public void CliRetInfoProcess(Socket cli, JsonData body)
        {
            Debug.Log("LogonEvent");
            
            string Ret = "Faild";
            bool state = DatabaseController.LogonMethod(body);
            if (state == true)
            {
                Ret = "Success";
            }
            
            string json = JsonController.Instance.JsonToString<string>("Logon", Ret);
            Debug.Log("dddddd: " + json);
            ServerContorller.Instance.SendToClient(cli, json);
        }
    }

    public class CloseEvent : IEvent
    {
        public void CliRetInfoProcess(Socket cli, JsonData body)
        {
            Debug.Log("Close");
            ServerContorller.Instance.SendToClient(cli, "Close Finish!");

            Thread.CurrentThread.Abort();
            ServerContorller.Instance.Exit = true;
        }
    }

}