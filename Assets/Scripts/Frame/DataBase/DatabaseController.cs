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
        if (CheckThisUserEx(body["name"]?.ToString()))
            return false;

        try
        {
            UserTable user = new UserTable();
            user.id = Kinder.users.Count;
            user.name = body["name"]?.ToString();
            user.password = body["password"]?.ToString();
            user.registry = body["registry"]?.ToString();
            user.login = body["login"]?.ToString();
            user.score = int.Parse(body["score"]?.ToString());
            user.score2 = int.Parse(body["score2"]?.ToString());
            user.score3 = int.Parse(body["score3"]?.ToString());

            Kinder.users.Add(user);
            return true;
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
            return false;
        }
    }

    public static bool LoginMethod(JsonData body)
    {
        try
        {
            return CheckThisUserEx(body["name"]?.ToString());
        }
        catch
        {
            return false;
        }
    }

    public static UserTable SearchUserUseName(JsonData body)
    {
        UserTable user = new UserTable();
        string name = body["name"]?.ToString();
        user = Kinder.users.Find(x => x.name == name);
        return user;
    }

    public static UserTable SearchUserUseID(JsonData body)
    {
        UserTable user = new UserTable();
        int ID = int.Parse(body["ID"]?.ToString());
        user = Kinder.users.Find(x => x.id == ID);
        return user;
    }

    public static bool UpdateUserInfo(JsonData body)
    {
        int ID = int.Parse(body["ID"]?.ToString());
        int idx = Kinder.users.FindIndex(x => x.id == ID);
        if (idx >= 0 && idx < Kinder.users.Count)
        {
            Kinder.users[idx].id = ID;
            try
            {
                Kinder.users[idx].score = int.Parse(body["score"]?.ToString());
                Kinder.users[idx].score2 = int.Parse(body["score2"]?.ToString());
                Kinder.users[idx].score3 = int.Parse(body["score3"]?.ToString());
                return true;
            }
            catch { return false; }
        }
        return false;
    }

    public static bool CheckThisUserEx(string name)
    {
        return Kinder.users.Exists(x => x.name == name);
    }
}
