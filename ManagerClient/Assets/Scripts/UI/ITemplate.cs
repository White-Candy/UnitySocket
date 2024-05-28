using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.InputField;

namespace CandySocket
{
    public enum TempCloseState
    {
        None,
        ChangeWin,
        CloseGame,
    }

    public abstract class AbstractTemplate
    {
        /// <summary>
        /// 确定按钮点击
        /// </summary>
        /// <param id="param[0]"> ID </param>
        /// <param name="param[1]"> Name </param>
        /// <param password="param[2]"> Password </param>
        /// <returns></returns>
        public abstract bool OnClicked(params object[] param);

        /// <summary>
        /// 在关闭前进行一次关闭状态的检查
        /// </summary>
        public abstract void CloseCheck();

        public abstract void Close();

        /// <summary>
        /// 窗口中 InputField 的初始化
        /// </summary>
        /// <param id="param[0]"> ID </param>
        /// <param name="param[1]"> Name </param>
        /// <param password="param[2]"> Password </param>
        /// <returns></returns>
        public abstract void Init(params object[] control);

        public TempCloseState CloseState;
    }

    public class Login : AbstractTemplate
    {
        public override bool OnClicked(params object[] param)
        {
            string name = param[1] as string;
            string password = param[2] as string;

            JsonData data = new JsonData();
            data["name"] = name;
            data["password"] = password;
            string json = JsonController.Instance.JsonToString("ManagerLogin", data);
            ClientController.Instance.Send(json); 
            return true;
        }

        public override void Init(params object[] control)
        {
            //Debug.Log("Login Init");
            InputField name = control[1] as InputField;
            InputField password = control[2] as InputField;
            password.contentType = ContentType.Password;

            name.text = "";
            password.text = "";
        }

        public override void CloseCheck()
        {
            CloseState = TempCloseState.CloseGame;
        }

        public override void Close()
        {
            if (CloseState == TempCloseState.CloseGame)
            {
                UIController.State = (int)UIState.US_None;
                UIController.TempState = TempUIState.TUS_None;
                Application.Quit();
            }
            else if (CloseState == TempCloseState.ChangeWin)
            {
                UIController.TempState = TempUIState.TUS_None;
            }
        }
    }

    public class Editor : AbstractTemplate
    {
        public override bool OnClicked(params object[] param)
        {
            string id = param[0] as string;
            string name = param[1] as string;
            string password = param[2] as string;

            JsonData data = new JsonData();
            data["id"] = id;
            data["name"] = name;
            data["password"] = password;
            string json = JsonController.Instance.JsonToString("ManagerEditor", data);
            ClientController.Instance.Send(json);
            return true;
        }

        public override void Init(params object[] control)
        {
            //Debug.Log("Editor Init");
            InputField id = control[0] as InputField;
            InputField name = control[1] as InputField;
            InputField password = control[2] as InputField;
            password.contentType = ContentType.Standard;

            id.text = GlobalParameterManager.UserInfo.id.ToString();
            name.text = GlobalParameterManager.UserInfo.name;
            password.text = GlobalParameterManager.UserInfo.password;
        }

        public override void CloseCheck()
        {
            
        }

        public override void Close()
        {
            // UIController.State = (int)UIState.US_Main;
            UIController.TempState = TempUIState.TUS_None;
        }

    }

    public class Add : AbstractTemplate
    {
        public override bool OnClicked(params object[] param)
        {
            Debug.Log("Add Clicked");
            
            string name = param[1] as string;
            string password = param[2] as string;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                GlobalParameterManager.MessageBox.SetStyle("添加异常", "文本不可为空", false, "确定");
                return false;
            }

            UserData user = new UserData(-1, name, password, DateTime.Now, new DateTime(), 0, 0, 0);
            JsonData data = JsonMapper.ToObject(JsonMapper.ToJson(user));
            string json = JsonController.Instance.JsonToString("Logon", data);
            ClientController.Instance.Send(json);
            return true;
        }

        public override void Init(params object[] control)
        {
            InputField id = control[0] as InputField;
            InputField name = control[1] as InputField;
            InputField password = control[2] as InputField;
            password.contentType = ContentType.Standard;

            id.text = "";
            name.text = "";
            password.text = "";
        }

        public override void CloseCheck()
        {
         
        }

        public override void Close()
        {
            UIController.TempState = TempUIState.TUS_None;
            //UIController.State = (int)UIState.US_Main;
        }

    }

    public static class TemplateSpawn
    {
        public static AbstractTemplate Create(TempUIState state)
        {
            switch(state)
            {
                case TempUIState.TUS_None:
                    return null;
                case TempUIState.TUS_Login:
                    return new Login();
                case TempUIState.TUS_Eidtor:
                    return new Editor();
                case TempUIState.TUS_Add:
                    return new Add();
                default:
                    break;
            }
            return null;
        }
    }
}