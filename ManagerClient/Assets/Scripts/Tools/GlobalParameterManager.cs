using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CandySocket
{
    public static class GlobalParameterManager
    {
        public static Queue<string> MessQueue = new Queue<string>();

        public static CandySocket.UserData UserInfo;

        public static TemplateWindow TemplateWindow = GameObject.Find("Canvas/EditorCanvas").GetComponent<TemplateWindow>();

        public static MainWindow MainWindow = GameObject.Find("Canvas/MainCanvas").GetComponent<MainWindow>();
    }
}


