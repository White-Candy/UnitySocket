using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandySocket
{
    public class UserData
    {
        public int id;
        public string name;
        public string password;
        public string registry;
        public string login;
        public int score;
        public int score2;
        public int score3;

        public UserData(int ID, string name, string password, DateTime registry, DateTime login, int score, int score2, int score3)
        {
            this.id = ID;
            this.name = name;
            this.password = password;
            this.registry = registry.ToString();
            this.login = login.ToString();
            this.score = score;
            this.score2 = score2;
            this.score3 = score3;
        }

        public UserData()
        {

        }

    }

    public class DataStructure
    {

    }
}