using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserTable
{
    public int id;
    public string name;
    public string password;
    public string registry;
    public string login;
    public int score;
    public int score2;
    public int score3;
}

[Serializable]
public class ManagerTable
{
    public string name;
    public string password;
}

[CreateAssetMenu(fileName = "Kinder", menuName = "DataBase/KinderDatabase")]
public class kinderDatabase : ScriptableObject
{
    public List<UserTable> users = new List<UserTable>();
    public List<ManagerTable> managers = new List<ManagerTable>();
}
