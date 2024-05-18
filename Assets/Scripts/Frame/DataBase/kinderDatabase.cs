using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserTable
{
    public int id;
    public string user;
    public string password;
    public int scores;
}

[CreateAssetMenu(fileName = "Kinder", menuName = "DataBase/KinderDatabase")]
public class kinderDatabase : ScriptableObject
{
    public List<UserTable> users = new List<UserTable>();
}
