﻿using LitJson;
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
        ManagerUsersInfo
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
            if (body["state"]?.ToString() == "Success")
            {
                JsonData data = new JsonData();
                data["body"] = "{req get users info}";
                string json = JsonController.Instance.JsonToString("ManagerUsersInfo", data);
                ClientController.Instance.Send(json);
                UIController.State = (int)UIState.US_Main;
            }
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

    public class ManagerUsersInfo : IEvent
    {
        public void OnEvent(JsonData body)
        {
            Debug.Log("ManagerUsersInfo: " + body.ToJson());

            string list = body.ToJson();
            List<UserData> users = new List<UserData>();
            users = JsonMapper.ToObject<List<UserData>>(list);

            foreach (var user in users)
            {
                //Debug.Log(user.id + " : " + user.name);
            }
        }
    }
}
