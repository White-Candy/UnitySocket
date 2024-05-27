using CandySocket;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : BaseUI
{

    public Button Ok;
    public Button Cancel;
    public Button Close;

    void Start()
    {
        UIController.winDic.Add(UIState.US_Message, this);

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
        UIController.State = (int)(UIState.US_Main);
    }
}
