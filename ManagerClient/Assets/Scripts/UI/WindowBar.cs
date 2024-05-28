using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class WindowBar
{
    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hwd, int cmdShow);

    [DllImport("user32.dll")]
    public static extern long GetWindowLong(IntPtr hwd, int nIndex);

    [DllImport("user32.dll")]
    public static extern void SetWindowLong(IntPtr hwd, int nIndex, long dwNewLong);

    const int SW_SHOWMINIMIZED = 2;

    /// <summary>
    /// 最大化
    /// </summary>
    const int SW_SHOWMAXIMIZED = 3;

    /// <summary>
    /// 还原
    /// </summary>
    const int SW_SHOWRESTORE = 1;

    /// <summary>
    /// 窗口风格
    /// </summary>
    const int GWL_STYLE = -16;
    /// <summary>
    /// 标题栏
    /// </summary>
    const int WS_CAPTION = 0x00c00000;
    /// <summary>
    /// 标题栏按钮
    /// </summary>
    const int WS_SYSMENU = 0x00080000;

    public static void MinWindow()
    {
        ShowWindow(GetForegroundWindow(), SW_SHOWMINIMIZED);
    }

    public static void HideWindowBar()
    {
        var hwd = GetForegroundWindow();
        var wl = GetWindowLong(hwd, GWL_STYLE);
        wl &= ~WS_CAPTION;
        SetWindowLong(hwd, GWL_STYLE, wl);
    }
}
