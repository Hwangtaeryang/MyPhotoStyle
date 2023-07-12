using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ProFileUserInfoManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField callInputField;

    public InputField virtualInputField;
    public VirtualKeyboard keyboard_s;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI callText;


    string virtualTextMode;


    void Start()
    {
        
    }

    public void Name_VirtualKeyBoardOpen()
    {
        virtualTextMode = "NameMode";

        if (nameInputField.text != "")
        {
            keyboard_s.Clear();
            virtualInputField.text = nameInputField.text;
        }
        else
        {
            keyboard_s.Clear();
            virtualInputField.text = "";
        }
    }

    public void Call_VirtualKeyBoardOpen()
    {
        virtualTextMode = "CallMode";

        if (callInputField.text != "")
        {
            keyboard_s.Clear();
            virtualInputField.text = callInputField.text;
        }
        else
        {
            keyboard_s.Clear();
            virtualInputField.text = "";
        }  
    }

    public void VirtualKeBoard_OK()
    {
        int num = PlayerPrefs.GetInt("MyPhotoStyle_ColorNumber");

        TextColor(num);

        if (virtualTextMode.Equals("NameMode"))
        {
            nameInputField.text = virtualInputField.text;
            nameText.text = virtualInputField.text;
        }
        else if(virtualTextMode.Equals("CallMode"))
        {
            callInputField.text = virtualInputField.text;
            callText.text = virtualInputField.text;
        }
    }

    void TextColor(int _num)
    {
        if(_num.Equals(0) || _num.Equals(7) || _num.Equals(8) || _num.Equals(9) || _num.Equals(12) ||
            _num.Equals(13) || _num.Equals(19) || _num.Equals(20) || _num.Equals(23) || _num.Equals(24))
        {
            nameText.color = new Color(0, 0, 0);
            callText.color = new Color(0, 0, 0);
        }
        else
        {
            nameText.color = new Color(255, 255, 255);
            callText.color = new Color(255, 255, 255);
        }
    }
}
