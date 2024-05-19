using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

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
        void CliRetInfoProcess(JsonData body);
    }

    public class LoginEvent : IEvent
    {
        public void CliRetInfoProcess(JsonData body)
        {
            Debug.Log("LoginEvent");
        }
    }

    public class LogonEvent : IEvent
    {
        public void CliRetInfoProcess(JsonData body)
        {
            Debug.Log("LogonEvent");
        }
    }

    public class CloseEvent : IEvent
    {
        public void CliRetInfoProcess(JsonData body)
        {
            Debug.Log("Close");

            Thread.CurrentThread.Abort();
            ServerContorller.Instance.Exit = true;
        }
    }

}