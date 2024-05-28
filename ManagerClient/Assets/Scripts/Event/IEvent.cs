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
        Logon,
        ManagerLogin,
        ManagerUsersInfo,
        ManagerEditor,
        ManagerDelete
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
            else
            {
                GlobalParameterManager.MessageBox.SetStyle("", "登录失败", false, "确定");
            }
        }
    }

    public class Logon : IEvent
    {
        public void OnEvent(JsonData body)
        {
            Debug.Log("Logon: " + body.ToJson());
            string state = body["state"]?.ToString();
            string hint;
            if (state == "Success")
            {
                hint = "添加成功！";

                //添加成功更新主面板
                JsonData data = new JsonData();
                data["body"] = "{req get users info}";
                string json = JsonController.Instance.JsonToString("ManagerUsersInfo", data);
                ClientController.Instance.Send(json);
            }
            else
            {
                hint = "添加失败！";
            }

            GlobalParameterManager.MessageBox.SetStyle("提示", hint, false, "确定", () => 
            {
                UIController.State = (int)(UIState.US_Main);
            });
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
                GlobalParameterManager.Items[idx].EditorItem(body["name"]?.ToString(), body["password"]?.ToString());
                UIController.TemplateClose = true;
            }
        }
    }

    public class ManagerDelete : IEvent
    {
        public void OnEvent(JsonData body)
        {
            Debug.Log("ManagerDelete : " + body.ToJson());
            if (body["state"]?.ToString() == "Success")
            {
                int idx = GlobalParameterManager.Items.FindIndex((x) =>
                        int.Parse(x.Id.GetComponentInChildren<Text>().text) == GlobalParameterManager.SelectId);
                GlobalParameterManager.Items[idx].DeleteItem();
                GlobalParameterManager.Items.RemoveAt(idx);
                GlobalParameterManager.MessageBox.gameObject.SetActive(false);
                UIController.State = (int)(UIState.US_Main);
            }
        }
    }
}
