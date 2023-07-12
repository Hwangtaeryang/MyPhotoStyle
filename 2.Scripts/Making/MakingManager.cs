using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;



public class MakingManager : MonoBehaviour
{
    public static MakingManager instance { get; private set; }



    public Image frameImg;

    public RawImage mainRawImg;
    public GameObject[] frames;

    public int clickIndex;

    string makingPicturePath;
    FileInfo[] makingPicturesData;
    Texture2D[] makingPictureTexture;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;
    }


    void Start()
    {
        makingPictureTexture = new Texture2D[4];

        makingPicturePath = Application.persistentDataPath + "/4.MakingPicture";

        DirectoryInfo di = new DirectoryInfo(makingPicturePath);
        makingPicturesData = di.GetFiles("*.png");

        for(int i = 0; i < 4; i++)
        {
            byte[] makingPictureByte = File.ReadAllBytes(Application.persistentDataPath +
                "/4.MakingPicture/" + makingPicturesData[i].Name);
            //Texture2D makingPictureTexture = null;
            makingPictureTexture[i] = new Texture2D(0, 0);
            makingPictureTexture[i].LoadImage(makingPictureByte);

            if (i != 3)
                frames[0].transform.GetChild(i).GetComponent<RawImage>().texture = makingPictureTexture[i];
            else mainRawImg.texture = makingPictureTexture[i];
        }
    }


    public void FrameShow(Sprite _sprite)
    {
        frameImg.sprite = _sprite;

        for(int i = 0; i < frames.Length; i++)
        {
            frames[i].SetActive(false);

            if (clickIndex.Equals(i))
            {
                frames[i].SetActive(true);
                for(int j = 0; j < 3; j++)
                    frames[i].transform.GetChild(j).GetComponent<RawImage>().texture = makingPictureTexture[j];
            } 
        }
    }
}
