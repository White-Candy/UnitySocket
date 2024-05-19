using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandySocket
{
    [Serializable]
    public class RetureClientBody
    {
        public string state;
        public string body;
    }

    [Serializable]
    public class ReciveClientBody
    {
        public EventType type;
        public JsonData body;
    }

    public class JsonController
    {
        private static JsonController m_instance;
        public static JsonController Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new JsonController();
                }
                return m_instance;
            }
        }

        public string JsonToString<T>(T obj, string state)
        {
            RetureClientBody body = new RetureClientBody();

            body.state = state;
            body.body = JsonMapper.ToJson(obj);
            return JsonMapper.ToJson(body);
        }

        public ReciveClientBody StringToJson(string json)
        {
            if (!checkStringIsJson(json)) return null;

            ReciveClientBody recive = new ReciveClientBody();

            JsonData data = JsonMapper.ToObject(json);
            JsonData body = checkStringIsJson(data["body"]?.ToString()) ? 
                JsonMapper.ToObject(data["body"]?.ToString()) : new JsonData();

            recive.type = (EventType)Enum.Parse(typeof(EventType), data["type"]?.ToString());
            recive.body = body;
            return recive;
        }

        public bool checkStringIsJson(string json)
        {
            try
            {
                var tmpObj = JsonMapper.ToObject(json);
                return true;
            }
            catch (FormatException fex)
            {
                //Invalid json format
                Console.WriteLine(fex);
                return false;
            }
            catch (Exception ex) //some other exception
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
