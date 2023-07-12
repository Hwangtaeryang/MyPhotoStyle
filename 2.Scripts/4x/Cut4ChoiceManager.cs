using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Cut4ChoiceManager : MonoBehaviour
{
    public static Cut4ChoiceManager instance { get; private set; }

    
    public Toggle[] toggles;
    public Button nextBtn;


    public int checkCount;


    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;
    }


    void Start()
    {
        nextBtn.interactable = false;
    }

    
    public void ToggleCheck()
    {
        if(checkCount >= 4)
        {
            foreach(var t in toggles)
            {
                if (t.isOn.Equals(false))
                    t.interactable = false;
            }
            nextBtn.interactable = true;
        }
        else
        {
            nextBtn.interactable = false;
            foreach(var t in toggles)
            {
                if (t.isOn.Equals(false))
                    t.interactable = true;
            }
        }
    }

    public void PictureChoiceNameSave()
    {
        int num = 0;

        for(int i = 0; i < toggles.Length; i++)
        {
            if(toggles[i].isOn.Equals(true))
            {
                num++;
                PlayerPrefs.SetString("MyPhotoStyle_PictureCheckName" + num, 
                    "BasicPicture" + (i + 1).ToString());
            }
        }
    }
}
