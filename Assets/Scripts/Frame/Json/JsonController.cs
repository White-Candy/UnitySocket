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
        public string type; //enum: EventType
        public string body; //exp: body: {state:Failed}
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

        public string JsonToString<T>(T obj, JsonData data)
        {
            RetureClientBody body = new RetureClientBody();
            body.body = data.ToJson();
            try
            {
                body.type = JsonMapper.ToJson(obj);
            }
            catch
            {
                string type = obj.ToString();
                body.type = type;
            }
            return JsonMapper.ToJson(body);
        }

        public ReciveClientBody StringToJson(string json)
        {
            if (!checkStringIsJson(json)) return null;

            ReciveClientBody recive = new ReciveClientBody();

            JsonData data = JsonMapper.ToObject(json);

            JsonData body = checkStringIsJson(data["body"]?.ToString()) ? 
                JsonMapper.ToObject(data["body"]?.ToString()) : null;

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
