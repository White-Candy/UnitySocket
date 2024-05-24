﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        US_Main = 1 << 2,
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
        public static ITemplate TemplateHandle;

        public static Dictionary<UIState, BaseUI> winDic = new Dictionary<UIState, BaseUI>();

        public static int State = (int)UIState.US_Eidtor;
        private static int CurrState = (int)UIState.US_None;

        public static TempUIState TempState = TempUIState.TUS_Login;
        private static TempUIState CurrTempState = TempUIState.TUS_None;

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

        public static void OnEditorWin()
        {
            if (TempState != CurrTempState)
            {
                CurrTempState = TempState;
                TemplateParam.EnterNewStatus(CurrTempState);
                GlobalParameterManager.TemplateWindow.CtrolStyle(TemplateParam.tag, TemplateParam.title, TemplateParam.idActive);

                TemplateHandle = TemplateSpawn.Create(CurrTempState);
            }
        }
    }
}