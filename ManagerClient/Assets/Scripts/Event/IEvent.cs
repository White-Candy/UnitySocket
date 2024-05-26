using LitJson;
using System;
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
            Debug.Log("ManagerUsersInfo: ");

            string list = body.ToJson();
            List<UserData> users = new List<UserData>();
            users = JsonMapper.ToObject<List<UserData>>(list);

            // Create Item
            int ItemPos = 0; //第一个Button的Y轴位置
            int ItemHeight = 40; //Button的高度
            int ItemCount = users.Count; //Button的数量
            GameObject item_par = GameObject.Find("Content");
            foreach (var user in users)
            {
                GameObject goClone = UnityEngine.Object.Instantiate(GlobalParameterManager.UserItem);
                goClone.transform.parent = item_par.transform;
                goClone.gameObject.SetActive(true);
                //Debug.Log(user.id + " : " + user.name);
            }
        }
    }
}
