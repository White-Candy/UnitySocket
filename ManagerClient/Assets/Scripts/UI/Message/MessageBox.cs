using CandySocket;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : BaseUI
{

    public Button Ok;
    public Button Cancel;
    public Button Close;

    public Text Message;
    public Text Title;

    private Action CloseCallback;

    void Start()
    {
        Ok.onClick.AddListener(() => 
        {
            JsonData data = new JsonData();
            data["id"] = GlobalParameterManager.SelectId;
            string json = JsonController.Instance.JsonToString("ManagerDelete", data);
            ClientController.Instance.Send(json);
        });

        Cancel.onClick.AddListener(() =>
        {
            CloseMessageBox();
        });

        Close.onClick.AddListener(() =>
        {
            CloseMessageBox();
        });
    }

    
    void Update()
    {
        
    }

    void CloseMessageBox()
    {
        this.gameObject.SetActive(false);
        if (CloseCallback != null ) CloseCallback();
    }

    public void SetStyle(string title, string hint, bool active, string btn_text, Action callback = null)
    {
        Title.text = title;
        Message.text = hint;
        Ok.gameObject.SetActive(active);
        Cancel.GetComponentInChildren<Text>().text = btn_text;

        this.gameObject.SetActive(true);
        if (callback != null ) CloseCallback = callback;
    }
}
