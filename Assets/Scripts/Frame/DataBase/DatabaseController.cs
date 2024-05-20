using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Experimental.UIElements.UxmlAttributeDescription;

public class DatabaseController : MonoBehaviour
{
    private static DatabaseController m_instance;
    public static DatabaseController Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new DatabaseController();
            }
            return m_instance;
        }
    }

    public kinderDatabase Kinder;

    public bool LogonMethod(JsonData body)
    {
        UserTable user = new UserTable();
        //user.id = Kinder.users.Count;
        //user.name = body["name"].ToString();
        //user.password = body["password"].ToString();
        //user.registry = body["registry"].ToString();
        //user.login = body["login"].ToString();
        //user.score = int.Parse(body["score"].ToString());
        //user.score2 = int.Parse(body["score2"].ToString());
        //user.score3 = int.Parse(body["score3"].ToString());
        Kinder.users.Add(user);
        return true;
        //try
        //{
            
        //}
        //catch(Exception ex)
        //{
        //    Debug.Log(ex.Message);
        //    return false;
        //}
    }
}
