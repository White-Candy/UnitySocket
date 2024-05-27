﻿using CandySocket;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWindow : BaseUI
{
    public GameObject Item;
    public Button Add;
    public Button Refresh;

    void Start()
    {
        UIController.winDic.Add(UIState.US_Main, this);

        Refresh.onClick.AddListener(() =>
        {
            RefreshInfo();
        });

        Add.onClick.AddListener(() =>
        {
            AddUser();
        });
    }

    void Update()
    {
        if (GlobalParameterManager.UsersInfo.Count > 0)
        {
            // Clear Item
            foreach (var item in GlobalParameterManager.Items)
            {
                item.DeleteItem();
            }
            GlobalParameterManager.Items.Clear();

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

    void RefreshInfo()
    {
        JsonData data = new JsonData();
        data["body"] = "{req get users info}";
        string json = JsonController.Instance.JsonToString("ManagerUsersInfo", data);
        ClientController.Instance.Send(json);
    }

    void AddUser()
    {
       
    }
}
