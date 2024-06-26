﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandySocket
{
    public static class EventSpawn
    {
        public static IEvent CreateEvent(EventType type)
        {
            switch (type)
            {
                case EventType.Login:
                    return new LoginEvent();
                case EventType.Logon: 
                    return new LogonEvent();
                case EventType.Close:
                    return new CloseEvent();
                case EventType.Search:
                    return new SearchEvent();
                case EventType.UpdateInfo:
                    return new UpdateInfo();
                case EventType.ManagerLogin:
                    return new ManagerLogin();
                case EventType.ManagerUsersInfo:
                    return new ManagerUsersInfo();
                case EventType.ManagerEditor:
                    return new ManagerEditor();
                case EventType.ManagerDelete:
                    return new ManagerDelete();
                default:
                    throw new ArgumentException("无效的策略类型");
            }
        }
    }
}