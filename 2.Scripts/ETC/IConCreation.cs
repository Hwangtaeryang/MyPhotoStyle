using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IConCreation : MonoBehaviour
{
    public static IConCreation instance { get; private set; }


    public Transform makeParentPos; //버튼 생성 위치
    public GameObject copyPrefab;   //복사할 원본 오브젝트
    GameObject copyObj;

    string iconImgPath;
    string frameImgPath;
    FileInfo[] iconImgData;
    FileInfo[] frameImgData;

    byte[] iconImgByte;
    byte[] frameImgByte;
    public int iconImgMaxIndex = 0;


    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;
    }

    void Start()
    {
        IConButtonCreation();
        StartCoroutine(FrameShowInit());
    }

    //아이콘버튼 생성
    void IConButtonCreation()
    {
        frameImgPath = Application.persistentDataPath + "/Frame/" +
            PlayerPrefs.GetString("MyPhotoStyle_PictureMode");

        DirectoryInfo di2 = new DirectoryInfo(frameImgPath);
        frameImgData = di2.GetFiles("*.png");

        iconImgPath = Application.persistentDataPath + "/Filter/" +
            PlayerPrefs.GetString("MyPhotoStyle_PictureMode");

        DirectoryInfo di = new DirectoryInfo(iconImgPath);
        iconImgData = di.GetFiles("*.png");
        iconImgMaxIndex = iconImgData.Length;

        //아이콘 갯수에 맞게 생성
        for(int i = 0; i < iconImgMaxIndex; i++)
        {
            copyObj = Instantiate(copyPrefab, makeParentPos);

            iconImgByte = File.ReadAllBytes(Application.persistentDataPath +
                "/Filter/" + PlayerPrefs.GetString("MyPhotoStyle_PictureMode") +
                "/" + iconImgData[i].Name);

            Texture2D iconTexture = null;
            iconTexture = new Texture2D(0, 0);
            iconTexture.LoadImage(iconImgByte);

            copyObj.GetComponent<Image>().sprite =
                Sprite.Create(iconTexture, new Rect(0, 0, iconTexture.width, iconTexture.height),
                new Vector2(0, 0));

            copyObj.name = "Button " + (i + 1).ToString();
        }
    }

    public void CheckImageView(int _index)
    {
        //전부 비선택으로 초기화
        for (int i = 0; i < makeParentPos.childCount; i++)
            makeParentPos.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);

        makeParentPos.GetChild(_index).transform.GetChild(0).gameObject.SetActive(true);
        FrameShow(_index);

        if(SceneManager.GetActiveScene().name.Equals("1_3_ColorProfile"))
            PlayerPrefs.SetInt("MyPhotoStyle_ColorNumber", _index);
    }

    IEnumerator FrameShowInit()
    {
        yield return new WaitForSeconds(0.5f);
        FrameShow(0);
    }
    
    void FrameShow(int _index)
    {
        byte[] frameByte = File.ReadAllBytes(Application.persistentDataPath +
            "/Frame/" + PlayerPrefs.GetString("MyPhotoStyle_PictureMode") + "/" + frameImgData[_index].Name);
        Texture2D frameTexture = null;
        frameTexture = new Texture2D(0, 0);
        frameTexture.LoadImage(frameByte);

        if(SceneManager.GetActiveScene().name.Equals("3_2_MakingFrame"))
        {
            MakingManager.instance.clickIndex = _index;
            MakingManager.instance.FrameShow
                (Sprite.Create(frameTexture, new Rect(0, 0, frameTexture.width, frameTexture.height),
                new Vector2(0, 0)));
        } 
        else
         GetPictureManager.instance.FrameShow
            (Sprite.Create(frameTexture, new Rect(0, 0, frameTexture.width, frameTexture.height), new Vector2(0, 0)));
    }
}
