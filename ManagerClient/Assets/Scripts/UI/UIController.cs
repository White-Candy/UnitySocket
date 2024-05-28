using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

namespace CandySocket
{
    public enum TempUIState
    {
        TUS_None,
        TUS_Login,
        TUS_Eidtor,
        TUS_Add
    }

    public enum UIState
    {
        US_None = 0,
        US_Eidtor = 1 << 0,
        US_Main = 1 << 1,
    }

    public static class TemplateParam // 作为EditorCanvs 修改控件状态的参数
    {
        public static string title; // 窗口的title
        public static string tag;  // 窗口确认按钮Text
        public static bool idActive; // ID Input是否显示

        public static void EnterNewStatus(TempUIState state)
        {
            switch (state)
            {
                case TempUIState.TUS_Login:
                    title = "  管理员登录";
                    tag = "登录";
                    idActive = false;
                    break;
                case TempUIState.TUS_Eidtor:
                    title = "  编辑学生";
                    tag = "确认";
                    idActive = true;
                    break;
                case TempUIState.TUS_Add:
                    title = "  添加学生";
                    tag = "确认";
                    idActive = false;
                    break;
                default:
                    break;
            }
        }
    }

    public static class UIController
    {
        public static AbstractTemplate TemplateHandle;

        public static Dictionary<UIState, BaseUI> winDic = new Dictionary<UIState, BaseUI>();

        public static int State = (int)UIState.US_Eidtor;
        private static int CurrState = (int)UIState.US_None;

        public static TempUIState TempState = TempUIState.TUS_Login;
        private static TempUIState CurrTempState = TempUIState.TUS_None;

        public static bool TemplateClose = false; //Template窗口关闭信号

        public static void OnGUI()
        {
            if (State != CurrState)
            {
                CurrState = State;
                foreach (var ui in winDic)
                {
                    if (((int)ui.Key & State) != 0)
                    {
                        ui.Value.gameObject.SetActive(true);
                    }
                    else
                    {
                        ui.Value.gameObject.SetActive(false);
                    }
                }
            }
        }

        public static void OnTemplateWin(InputField id, InputField name, InputField pwd)
        {
            if (TempState != CurrTempState)
            {
                //Debug.Log("Change CurrTempState");
                CurrTempState = TempState;
                TemplateParam.EnterNewStatus(CurrTempState);
                GlobalParameterManager.TemplateWindow.CtrolStyle(TemplateParam.tag, TemplateParam.title, TemplateParam.idActive);

                TemplateHandle = TemplateSpawn.Create(CurrTempState);
                if (TemplateHandle != null)
                {
                    TemplateHandle.Init(id, name, pwd);
                }
            }
        }

        public static bool TempStateIsEquie()
        {
            return TempState == CurrTempState;
        }
    }
}
