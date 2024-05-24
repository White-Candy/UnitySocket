using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace CandySocket
{
    public enum EventType
    {
        None,
        ManagerLogin,
        EditorInfo,
        AddInfo,
    }

    public interface IEvent
    {
        void OnEvent(JsonData mess);
    }

    public class ManagerLogin : IEvent
    {
        public void OnEvent(JsonData body)
        {
            Debug.Log("ManagerLogin: " + body.ToJson());
        }
    }

    public class EditorInfo : IEvent
    {
        public void OnEvent(JsonData body)
        {
            Debug.Log("EditorInfo: " + body.ToJson());
        }
    }

    public class AddInfo : IEvent
    {
        public void OnEvent(JsonData body)
        {
            Debug.Log("ManaAddInfOgerLogin: " + body.ToJson());
        }
    }
}
