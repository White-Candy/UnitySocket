using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CandySocket;

public class TemplateWindow : BaseUI
{
    public Text txTitle;

    public Button btnClose;
    public Button btnOk;

    public GameObject ID;
    public GameObject Name;
    public GameObject Pwd;
    public Text Hint;

    private InputField inputID;
    private InputField inputName;
    private InputField inputPwd;

    void Awake()
    {
        inputID = ID.gameObject.GetComponentInChildren<InputField>();
        inputName = Name.gameObject.GetComponentInChildren<InputField>();
        inputPwd = Pwd.gameObject.GetComponentInChildren<InputField>();
    }

    void Start()
    {
        btnClose.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });

        btnOk.onClick.AddListener(() =>
        {
            if (checkInputIsNull(inputID) || checkInputIsNull(inputName) 
                || checkInputIsNull(inputPwd))
            {
                SetHint("输入框不可为空");
            }
            else
            {
                UIController.TemplateHandle.OnClicked(inputID.text, inputName.text, inputPwd.text);
            }
        });

        UIController.winDic.Add(UIState.US_Eidtor, this);
    }

    void Update()
    {
        UIController.OnEditorWin();
    }

    public void CtrolStyle(string tag, string title, bool id_active)
    {
        btnOk.GetComponentInChildren<Text>().text = tag;
        ID.gameObject.SetActive(id_active);  
        txTitle.text = title;
    }

    public void SetHint(string hint)
    {
        Hint.text = hint;
    }

    public bool checkInputIsNull(InputField input)
    {
        return input.IsActive() && string.IsNullOrEmpty(input.text);
    }
}
