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
                case EventType.EditorInfo:
                    return new EditorInfo();
                case EventType.AddInfo:
                    return new AddInfo();
                default:
                    break;
            }
            return null;
        }
    }
}