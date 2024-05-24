using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandySocket
{
    [Serializable]
    public class RetureServerBody
    {
        public string type; //enum: EventType
        public string body; //exp: body: {state:Failed}
    }

    [Serializable]
    public class ReciveServerBody
    {
        public EventType type;
        public JsonData body; // exp: body:{xxx:xxx}
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
            RetureServerBody body = new RetureServerBody();
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

        public ReciveServerBody StringToJson(string json)
        {
            if (!checkStringIsJson(json)) return null;

            ReciveServerBody recive = new ReciveServerBody();

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
                Debug.Log(fex);
                return false;
            }
            catch (Exception ex) //some other exception
            {
                //Debug.Log(ex.ToString());
                return false;
            }
        }
    }
}
