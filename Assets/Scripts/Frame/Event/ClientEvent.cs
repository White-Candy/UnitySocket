﻿using LitJson;
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
        Search,
        UpdateInfo,
        ManagerLogin,
        ManagerUsersInfo,
        ManagerEditor,
        ManagerDelete
    }

    public interface IEvent
    {
        void CliRetInfoProcess(Socket cli, JsonData body);
    }

    public class LoginEvent : IEvent
    {
        public void CliRetInfoProcess(Socket cli, JsonData body)
        {
            Debug.Log("LoginEvent: " + body.ToJson());

            string Ret = "Faild";
            bool state = DatabaseController.LoginMethod(body);
            UserTable user = DatabaseController.SearchUserUseName(body);
            JsonData data = new JsonData();
            if (state == true)
            {
                Ret = "Success";
                data["user_id"] = user.id.ToString();
            }

            data["state"] = Ret;

            string json = JsonController.Instance.JsonToString("Login", data);
            ServerContorller.Instance.SendToClient(cli, json);
        }
    }

    public class LogonEvent : IEvent
    {
        public void CliRetInfoProcess(Socket cli, JsonData body)
        {
            string Ret = "Faild";
            bool state = DatabaseController.LogonMethod(body);
            if (state == true)
            {
                Ret = "Success";
            }

            JsonData data = new JsonData();
            data["state"] = Ret;
            string json = JsonController.Instance.JsonToString("Logon", data);
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

    public class SearchEvent : IEvent
    {
        public void CliRetInfoProcess(Socket cli, JsonData body)
        {
            Debug.Log("Search");
            UserTable user = DatabaseController.SearchUserUseID(body);
            JsonData data = JsonMapper.ToObject(JsonMapper.ToJson(user));       
            string json = JsonController.Instance.JsonToString("Search", data);
            ServerContorller.Instance.SendToClient(cli, json);
        }
    }

    public class UpdateInfo : IEvent
    {
        public void CliRetInfoProcess(Socket cli, JsonData body)
        {
            Debug.Log("UpdateInfo");
            bool is_update = DatabaseController.UpdateUserInfo(body);
            string Ret = "Faild";

            if (is_update)
            {
                Ret = "Success";
            }
            JsonData data = new JsonData();
            data["state"] = Ret;
            string json = JsonController.Instance.JsonToString("UpdateInfo", data);
            ServerContorller.Instance.SendToClient(cli, json);
        }
    }

    public class ManagerLogin : IEvent
    {
        public void CliRetInfoProcess(Socket cli, JsonData body)
        {
            Debug.Log("ManagerLogin");

            string Ret = "Faild";
            bool state = DatabaseController.ManagerLoginMethod(body);
            JsonData data = new JsonData();
            if (state == true)
            {
                Ret = "Success";
            }
            data["state"] = Ret;

            string json = JsonController.Instance.JsonToString("ManagerLogin", data);
            ServerContorller.Instance.SendToClient(cli, json);
        }
    }

    public class ManagerUsersInfo : IEvent
    {
        public void CliRetInfoProcess(Socket cli, JsonData body)
        {
            Debug.Log("ManagerUsersInfo");

            List<UserTable> users = DatabaseController.GetUsersInfo();
            JsonData data = new JsonData();
            data["body"] = JsonMapper.ToJson(users);
            data["type"] = "ManagerUsersInfo";
            string json = JsonMapper.ToJson(data);
            ServerContorller.Instance.SendToClient(cli, json);
        }
    }

    public class ManagerEditor : IEvent
    {
        public void CliRetInfoProcess(Socket cli, JsonData body)
        {
            Debug.Log("ManagerEditor");

            string Ret = "Faild";
            bool state = DatabaseController.EditorUserInfo(body);
            JsonData data = new JsonData();
            if (state == true)
            {
                Ret = "Success";
            }
            data["state"] = Ret;
            data["name"] = body["name"]?.ToString();
            data["password"] = body["password"]?.ToString();

            string json = JsonController.Instance.JsonToString("ManagerEditor", data);
            GlobalParam.uimessage.AdditionalContent(json);
            ServerContorller.Instance.SendToClient(cli, json);
        }
    }

    public class ManagerDelete : IEvent
    {
        public void CliRetInfoProcess(Socket cli, JsonData body)
        {
            Debug.Log("ManagerDelete");
            string Ret = "Faild";
            bool state = DatabaseController.DeleteUserInfo(body);
            JsonData data = new JsonData();
            if (state == true)
            {
                Ret = "Success";
            }
            data["state"] = Ret;

            string json = JsonController.Instance.JsonToString("ManagerDelete", data);
            GlobalParam.uimessage.AdditionalContent(json);
            ServerContorller.Instance.SendToClient(cli, json);
        }
    }
}