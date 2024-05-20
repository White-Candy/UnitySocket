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
        Logon,
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
            //if (body == null) return;

            string Ret = "Faild";
            bool new_user = DatabaseController.Instance.LogonMethod(body);
            if (new_user == true)
            {
                Debug.Log("is Not -1");
                //DatabaseController.Kinder.users.Add(new_user);
                Ret = "Success";
            }
                      
            Debug.Log("LogonEvent JsonToString");
            string json = JsonController.Instance.JsonToString<string>("Logon", Ret);
            Debug.Log("LogonEvent: " + json);
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