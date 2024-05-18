using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseController : MonoBehaviour
{
    // Start is called before the first frame update
    private static kinderDatabase m_kiner;

    public static kinderDatabase Kinder
    {
        get
        {
            if (m_kiner == null)
            {
                m_kiner = Resources.Load("DataBase/KinderDatabase") as kinderDatabase;
            }
            return m_kiner;
        }
    }
}
