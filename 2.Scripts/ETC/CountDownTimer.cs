using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    public static CountDownTimer instance;

    public Image timerRot;
    public Image countImg;
    public Sprite[] countSprites;

    int pictureCount = 0;

    
    void Start()
    {
        instance = this;
        StartCoroutine(_FiveCountDown());
    }

    public static int PictureCount()
    {
        return instance.pictureCount;
    }

    IEnumerator _FiveCountDown()
    {
        yield return new WaitForSeconds(0.5f);

        timerRot.transform.DORotate(new Vector3(0f, 0f, -360), 2.5f, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear).SetLoops(-1);

        if (pictureCount != 1)
            yield return new WaitForSeconds(1f);

        countImg.sprite = countSprites[0];
        GameManager.instance.SFXSound("Countdown");
        yield return new WaitForSeconds(1f);

        countImg.sprite = countSprites[1];
        MakingPicture();
        GameManager.instance.SFXSound("Countdown");
        yield return new WaitForSeconds(1f);

        countImg.sprite = countSprites[2];
        MakingPicture();
        GameManager.instance.SFXSound("Countdown");
        yield return new WaitForSeconds(1f);

        countImg.sprite = countSprites[3];
        MakingPicture();
        GameManager.instance.SFXSound("Countdown");
        yield return new WaitForSeconds(1f);

        countImg.sprite = countSprites[4];
        GameManager.instance.SFXSound("Countdown_end");

        if (SceneManager.GetActiveScene().name.Equals("1_1_PictureShot"))
        {
            UICameraPictureShot.Static_UiTakePictureShot(1600, 2400, 0);
            yield return new WaitForSeconds(1f);
            StartCoroutine(_SceneManager());
        }
        else if(SceneManager.GetActiveScene().name.Equals("3_1_PictureShot"))
        {
            if(PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Making"))
            {
                pictureCount++;
                UICameraPictureShot.Static_UiTakePictureShot(1600, 2400, pictureCount);

                if (pictureCount >= 4)
                    StartCoroutine(_SceneManager());
            }
            else if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("4x"))
            {
                pictureCount++;
                UICameraPictureShot.Static_UiTakePictureShot(1600, 2400, pictureCount);
                yield return new WaitForSeconds(1f);

                if (pictureCount < 6)
                    StartCoroutine(_FiveCountDown());
                else StartCoroutine(_SceneManager());
            }
        }
    }

    //메이킹버전 사진찍기 - 5초안에 3컷 자동 촬영
    void MakingPicture()
    {
        if (SceneManager.GetActiveScene().name.Equals("3_1_PictureShot"))
        {
            if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Making"))
            {
                pictureCount++;
                UICameraPictureShot.Static_UiTakePictureShot(1600, 2400, pictureCount);
            }
        }
    }

    IEnumerator _SceneManager()
    {
        yield return null;

        if (SceneManager.GetActiveScene().name.Equals("1_1_PictureShot"))
            LoadScene.instance.LoadByName("1_2_Filter");
        else if (SceneManager.GetActiveScene().name.Equals("3_1_PictureShot"))
        {
            if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Making"))
                LoadScene.instance.LoadByName("3_2_MakingFrame");
            else if(PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("4x"))
                LoadScene.instance.LoadByName("4_1_Cut4Choice");
        }
            
    }
}
