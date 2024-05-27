using CandySocket;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    public Button Id;
    public Button Name;
    public Button Password;
    public Button Logon;
    public Button Login;
    public Button TxScroe;
    public Button TxScroe2;
    public Button TxScroe3;
    public Button Edit;
    public Button Delete;

    public void Init(int id, string name, string password, string logon, string login, int Scroe, int Scroe2, int Score3)
    {
        Id.GetComponentInChildren<Text>().text = id.ToString();
        Name.GetComponentInChildren<Text>().text = name;
        Password.GetComponentInChildren<Text>().text = password;
        Logon.GetComponentInChildren<Text>().text = logon;
        Login.GetComponentInChildren<Text>().text = login;
        TxScroe.GetComponentInChildren<Text>().text = Scroe.ToString();
        TxScroe2.GetComponentInChildren<Text>().text = Scroe2.ToString();
        TxScroe3.GetComponentInChildren<Text>().text = Score3.ToString();

        Edit.onClick.AddListener(() => { EditClicked(); });
        Delete.onClick.AddListener(() => { DeleteClicked(); });

        GlobalParameterManager.Items.Add(this);
    }

    private void EditClicked()
    {
        UserData user = new UserData();
        user.id = int.Parse(Id.GetComponentInChildren<Text>().text);
        user.name = Name.GetComponentInChildren<Text>().text;
        user.password = Password.GetComponentInChildren<Text>().text;
        GlobalParameterManager.UserInfo = user;
        GlobalParameterManager.SelectId = user.id;

        UIController.TempState = TempUIState.TUS_Eidtor;
        UIController.State = (int)(UIState.US_Eidtor | UIState.US_Main);
    }

    private void DeleteClicked()
    {
        //Debug.Log("Delete: " + Name.GetComponentInChildren<Text>().text);
    }

    public void EditorItem(string name, string password)
    {
        Name.GetComponentInChildren<Text>().text = name;
        Password.GetComponentInChildren<Text>().text = password;
    }
}
