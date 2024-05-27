using LitJson;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace CandySocket
{
    public enum EventType
    {
        None,
        ManagerLogin,
        EditorInfo,
        AddInfo,
        ManagerUsersInfo,
        ManagerEditor
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
            Debug.Log("ManagerUsersInfo: ");
            string list = body.ToJson();
            GlobalParameterManager.UsersInfo = JsonMapper.ToObject<List<UserData>>(list);
        }
    }

    public class ManagerEditor : IEvent
    {
        public void OnEvent(JsonData body)
        {
            Debug.Log("ManagerEditor : " + body.ToJson());
            if (body["state"]?.ToString() == "Success")
            {
                int idx = GlobalParameterManager.Items.FindIndex((x) => 
                        int.Parse(x.Id.GetComponentInChildren<Text>().text) == GlobalParameterManager.SelectId);
                Debug.Log(idx + " | " + GlobalParameterManager.SelectId);
                GlobalParameterManager.Items[idx].EditorItem(body["name"]?.ToString(), body["password"]?.ToString());
                UIController.TemplateClose = true;
            }
        }
    }
}
