using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandySocket
{
    public static class EventSpawn
    {
        public static IEvent CreateEvent(EventType type)
        {
            switch(type)
            {
                case EventType.ManagerLogin:
                    return new ManagerLogin();
                case EventType.AddInfo:
                    return new AddInfo();
                case EventType.ManagerUsersInfo:
                    return new ManagerUsersInfo();
                case EventType.ManagerEditor:
                    return new ManagerEditor();
                case EventType.ManagerDelete:
                    return new ManagerDelete();
                default:
                    break;
            }
            return null;
        }
    }
}