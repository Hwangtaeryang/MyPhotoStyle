using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonCtrl : MonoBehaviour
{
    public Image titleImg;
    public Sprite[] titleSprite;


    private void Start()
    {
        if(SceneManager.GetActiveScene().name != "0_Boot" && SceneManager.GetActiveScene().name != "0_Main")
        {
            if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Profile"))
                titleImg.sprite = titleSprite[0];
            else if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Celebrity"))
                titleImg.sprite = titleSprite[1];
            else if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Making"))
                titleImg.sprite = titleSprite[2];
            else if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("4x"))
                titleImg.sprite = titleSprite[3];
        }
        
    }

    public void MoveScene(string _name)
    {
        GameManager.instance.SFXSound("ClickSound");
        SceneManager.LoadScene(_name);
    }

    public void PictureMode(string _mode)
    {
        PlayerPrefs.SetString("MyPhotoStyle_PictureMode", _mode);
    }

    public void PictureSave()
    {
        if(SceneManager.GetActiveScene().name.Equals("3_3_Filters"))
        {
            GameObject.Find("UICanvas 1").gameObject.SetActive(false);
            GameObject.Find("UICanvas 2").gameObject.SetActive(false);
            GameObject.Find("UICanvas 3").gameObject.SetActive(false);
            GameObject.Find("UICanvas 4").gameObject.SetActive(false);
            GameObject.Find("UICamera 1").gameObject.SetActive(false);
            GameObject.Find("UICamera 2").gameObject.SetActive(false);
            GameObject.Find("UICamera 3").gameObject.SetActive(false);
            GameObject.Find("UICamera 4").gameObject.SetActive(false);
        }
        UICameraPictureShot.Static_UiTakePictureShot(1600, 2400, 0);
    }

    public void FramePick2_2Scene()
    {
        if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Making"))
            SceneManager.LoadScene("1_2_Filter");
        else if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("4x"))
            SceneManager.LoadScene("4_2_Filter");
    }

    public void PrintScene()
    {
        if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Profile"))
            SceneManager.LoadScene("1_4_UserInfoSave");
        else if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Celebrity"))
            SceneManager.LoadScene("2_1_FramePick");
        else if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Making"))
            SceneManager.LoadScene("3_3_Filters");
        else if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("4x"))
            SceneManager.LoadScene("2_1_FramePick");
    }

    public void SystemExit()
    {
        GameManager.instance.SFXSound("ClickSound");
        Application.Quit();
    }
}
