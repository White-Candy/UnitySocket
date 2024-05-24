using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandySocket
{
    public interface ITemplate
    {
        /// <summary>
        /// 确定按钮点击
        /// </summary>
        /// <param name="param[0]"> ID </param>
        /// <param name="param[1]"> Name </param>
        /// <param name="param[2]"> Password </param>
        /// <returns></returns>
        bool OnClicked(params object[] param);
    }

    public class Login : ITemplate
    {
        public bool OnClicked(params object[] param)
        {
            Debug.Log("Login Clicked");
            string name = param[1] as string;
            string password = param[2] as string;

            JsonData data = new JsonData();
            data["name"] = name;
            data["password"] = password;
            string json = JsonController.Instance.JsonToString("ManagerLogin", data);
            ClientController.Instance.Send(json); 
            return true;
        }
    }

    public class Editor : ITemplate
    {
        public bool OnClicked(params object[] param)
        {
            Debug.Log("Editor Clicked");
            return true;
        }
    }

    public class Add : ITemplate
    {
        public bool OnClicked(params object[] param)
        {
            Debug.Log("Add Clicked");
            return true;
        }
    }

    public static class TemplateSpawn
    {
        public static ITemplate Create(TempUIState state)
        {
            ITemplate template = null;
            switch(state)
            {
                case TempUIState.TUS_Login:
                    template = new Login();
                    break;
                case TempUIState.TUS_Eidtor:
                    template = new Editor();
                    break;
                case TempUIState.TUS_Add:
                    template = new Add();
                    break;
                default:
                    break;
            }
            return template;
        }
    }
}