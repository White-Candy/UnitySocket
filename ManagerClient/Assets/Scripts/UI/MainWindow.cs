using CandySocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWindow : BaseUI
{
    public GameObject Item;

    void Start()
    {
        UIController.winDic.Add(UIState.US_Main, this);
    }

    void Update()
    {
        if (GlobalParameterManager.UsersInfo.Count > 0)
        {
            // Create Item
            GameObject item_par = GameObject.Find("Content");
            foreach (var user in GlobalParameterManager.UsersInfo)
            {
                GameObject goClone = Instantiate(Item);
                goClone.transform.parent = item_par.transform;
                goClone.gameObject.SetActive(true);
                ItemScript script = goClone.GetComponent<ItemScript>();
                script.Init(user.id, user.name, user.password, user.registry,
                    user.login, user.score, user.score2, user.score3);
            }
            GlobalParameterManager.UsersInfo.Clear();
        }
    }
}
