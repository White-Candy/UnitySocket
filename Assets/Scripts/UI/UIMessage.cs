using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class UIMessage : MonoBehaviour
{
    private Text Message;

    void Awake()
    {
        Message = GameObject.Find("UI/Image/Text").GetComponent<Text>();
    }


    void Update()
    {
    }

    public void AdditionalContent(string mess)
    {
        if (Message)
            Message.text += ("ServerLog># " + mess + "\n\n");
    }
}
