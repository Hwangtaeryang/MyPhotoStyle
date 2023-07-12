using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PictureCountNumber : MonoBehaviour
{
    public TextMeshProUGUI totalCount;
    public TextMeshProUGUI pictureCount;
    public GameObject countDownObj;

    int count = 0;

    void Start()
    {
        if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Making"))
            totalCount.text = "/ 4";
        else if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("4x"))
            totalCount.text = "/ 6";

        pictureCount.text = count.ToString();
    }

    private void FixedUpdate()
    {
        if(countDownObj.activeSelf.Equals(true))
        {
            if(count != CountDownTimer.PictureCount())
            {
                count = CountDownTimer.PictureCount();
                pictureCount.text = count.ToString();
            }
        }
    }
}
