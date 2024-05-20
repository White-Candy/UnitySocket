using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Experimental.UIElements.UxmlAttributeDescription;

public static class DatabaseController
{
    private static kinderDatabase m_kinder;
    public static kinderDatabase Kinder
    {
        get
        {
            if (m_kinder == null)
            {
                m_kinder = Resources.Load("DataBase/KinderDatabase") as kinderDatabase;
            }
            return m_kinder;
        }
    }

    public static bool LogonMethod(JsonData body)
    {
        try
        {
            UserTable user = new UserTable();
            user.id = Kinder.users.Count;
            //user.name = body["name"]?.ToString();
            //user.password = body["password"]?.ToString();
            //user.registry = body["registry"]?.ToString();
            //user.login = body["login"]?.ToString();
            //user.score = int.Parse(body["score"]?.ToString());
            //user.score2 = int.Parse(body["score2"]?.ToString());
            //user.score3 = int.Parse(body["score3"]?.ToString());

            Kinder.users.Add(user);
            return true;
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
            return false;
        }
    }
}
