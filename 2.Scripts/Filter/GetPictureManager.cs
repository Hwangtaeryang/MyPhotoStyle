using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetPictureManager : MonoBehaviour
{
    public static GetPictureManager instance { get; private set; }


    public Image frameImg;

    public RawImage uiRawImg;
    public RawImage[] rawImgs;
    byte[] pictureImgBtye;


    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;
    }

    void Start()
    {
        if(SceneManager.GetActiveScene().name.Equals("4_1_Cut4Choice"))
        {
            for (int i = 0; i < 6; i++)
            {
                pictureImgBtye =
            File.ReadAllBytes(Application.persistentDataPath + "/4.MakingPicture/BasicPicture" + (i + 1).ToString() + ".png");

                Texture2D textures = null;
                textures = new Texture2D(0, 0);
                textures.LoadImage(pictureImgBtye);

                rawImgs[i].texture = textures;
            }
        }
        else if(SceneManager.GetActiveScene().name.Equals("4_2_Filter"))
        {
            for(int i = 0; i < 4; i++)
            {
                pictureImgBtye =
                    File.ReadAllBytes(Application.persistentDataPath + "/4.MakingPicture/" +
                    PlayerPrefs.GetString("MyPhotoStyle_PictureCheckName" + (i + 1).ToString()) + ".png");

                Texture2D textures = null;
                textures = new Texture2D(0, 0);
                textures.LoadImage(pictureImgBtye);

                rawImgs[i].texture = textures;
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().name.Equals("1_2_Filter"))
                pictureImgBtye =
                File.ReadAllBytes(Application.persistentDataPath + "/0.BasicPicture/BasicPicture.png");
            else if (SceneManager.GetActiveScene().name.Equals("1_3_ColorProfile") ||
                SceneManager.GetActiveScene().name.Equals("2_1_FramePick"))
                pictureImgBtye =
                File.ReadAllBytes(Application.persistentDataPath + "/1.FilterPicture/FilterPicture.png");
            else if (SceneManager.GetActiveScene().name.Equals("1_4_UserInfoSave"))
                pictureImgBtye =
                    File.ReadAllBytes(Application.persistentDataPath + "/2.ColorPicture/ColorPicture.png");
            else if (SceneManager.GetActiveScene().name.Equals("0_Print"))
                pictureImgBtye =
                    File.ReadAllBytes(Application.persistentDataPath + "/6.Print/PrintPicture.png");
            else if (SceneManager.GetActiveScene().name.Equals("3_3_Filters"))
            {
                for (int i = 0; i < 4; i++)
                {
                    pictureImgBtye =
                File.ReadAllBytes(Application.persistentDataPath + "/4.MakingPicture/BasicPicture" + (i + 1).ToString() + ".png");

                    Texture2D textures = null;
                    textures = new Texture2D(0, 0);
                    textures.LoadImage(pictureImgBtye);

                    rawImgs[i].texture = textures;
                }

                pictureImgBtye =
                File.ReadAllBytes(Application.persistentDataPath + "/3.FramePicture/FramePicture.png");
            }

            Texture2D texture = null;
            texture = new Texture2D(0, 0);
            texture.LoadImage(pictureImgBtye);

            uiRawImg.texture = texture;
        }
    }

    public void FrameShow(Sprite _sprite)
    {
        frameImg.sprite = _sprite;
    }
}
