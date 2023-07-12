using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FilterCreation : MonoBehaviour
{
    public static FilterCreation instance { get; private set; }

    public Transform makeParentPos; //필터버튼 생성 위치
    public GameObject copyPrefab;   //복사할 원본 오브젝트
    GameObject copyObj;

    string filterImgPath;
    string filterPath;
    FileInfo[] filterImgData;
    FileInfo[] filterData;

    byte[] filterImgByte;

    public int filterImgMaxIndex = 0;



    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;
    }

    void Start()
    {
        FilterButtonCreation();
    }

    //필터버튼 생성
    void FilterButtonCreation()
    {
        filterImgPath = Application.persistentDataPath + "/Filter/FilterButtonImage";

        DirectoryInfo di = new DirectoryInfo(filterImgPath);
        filterImgData = di.GetFiles("*.png");
        filterImgMaxIndex = filterImgData.Length;
        
        //핉 갯수에 맞게 생성하기
        for(int i =0; i < filterImgMaxIndex; i++)
        {
            copyObj = Instantiate(copyPrefab, makeParentPos);

            filterImgByte = File.ReadAllBytes(Application.persistentDataPath + "/Filter/FilterButtonImage/"
                + filterImgData[i].Name);

            Texture2D filterTexture = null;
            filterTexture = new Texture2D(0, 0);
            filterTexture.LoadImage(filterImgByte);

            copyObj.GetComponent<Image>().sprite =
                Sprite.Create(filterTexture, new Rect(0, 0, filterTexture.width, filterTexture.height), new Vector2(0, 0));

            copyObj.name = "Button " + (i + 1).ToString();
        }
    }

    public Texture FilterChoiceViewShow(int _index)
    {
        filterPath = Application.persistentDataPath + "/Filter/Filter";
        DirectoryInfo di = new DirectoryInfo(filterPath);
        filterData = di.GetFiles("*.png");

        byte[] filterByte =
            File.ReadAllBytes(Application.persistentDataPath + "/Filter/Filter/" + filterData[_index].Name);
        Texture2D filterTexture = null;
        filterTexture = new Texture2D(0, 0);
        filterTexture.LoadImage(filterByte);

        return filterTexture;
    }

    public void CheckImageView(int _index)
    {
        for(int i = 0; i < makeParentPos.childCount; i++)
        {
            makeParentPos.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
        }

        makeParentPos.GetChild(_index).transform.GetChild(0).gameObject.SetActive(true);
    }
}
