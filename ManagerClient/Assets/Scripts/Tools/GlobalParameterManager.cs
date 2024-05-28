using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CandySocket
{
    public static class GlobalParameterManager
    {
        public static Queue<string> MessQueue = new Queue<string>();

        public static CandySocket.UserData UserInfo = new UserData();

        public static TemplateWindow TemplateWindow;// = GameObject.Find("Canvas/EditorCanvas").GetComponent<TemplateWindow>();
        public static MainWindow MainWindow;// = GameObject.Find("Canvas/MainCanvas").GetComponent<MainWindow>();
        public static MessageBox MessageBox;

        public static List<UserData> UsersInfo = new List<UserData>(); //所有用户的基本信息

        public static List<ItemScript> Items = new List<ItemScript>(); //主界面中的所有Item

        public static int SelectId = -1; //选择item的用户id

        public static string Message; // 接受服务器分段信息存储
    }
}


