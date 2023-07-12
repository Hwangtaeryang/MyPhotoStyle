using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class UICameraPictureShot : MonoBehaviour
{
    private static UICameraPictureShot instance;

    //public RenderTexture verticalRender;
    Camera uiCamera;
    bool takeScereenShotOnNextFrame;

    int pictureCount;


    void Start()
    {
        instance = this;
        uiCamera = gameObject.GetComponent<Camera>();
    }


    private void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }


    private void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }


    void RenderPipelineManager_endCameraRendering(ScriptableRenderContext _context, Camera _camera)
    {
        OnPostRender();
    }

    private void OnPostRender()
    {
        if(takeScereenShotOnNextFrame)
        {
            
            takeScereenShotOnNextFrame = false;
            RenderTexture renderTexture = uiCamera.targetTexture;

            Texture2D renderResult = null;
            renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();

            FolderPath(byteArray);

            RenderTexture.ReleaseTemporary(renderTexture);
            //uiCamera.targetTexture = null;
            //uiCamera.targetTexture = verticalRender;
        }
    }

    void FolderPath(byte[] _byteArray)
    {
        if(SceneManager.GetActiveScene().name.Equals("1_1_PictureShot"))
        {
            string folderPath = Application.persistentDataPath + "/0.BasicPicture";

            //폴더가 없으면 생성
            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + "/0.BasicPicture/BasicPicture.png", _byteArray);
        }
        else if(SceneManager.GetActiveScene().name.Equals("3_1_PictureShot"))
        {
            string folderPath = Application.persistentDataPath + "/4.MakingPicture";

            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath +
                "/4.MakingPicture/BasicPicture" + pictureCount + ".png", _byteArray);
        }
        else if(SceneManager.GetActiveScene().name.Equals("1_2_Filter") ||
            SceneManager.GetActiveScene().name.Equals("4_2_Filter"))
        {
            string folderPath = Application.persistentDataPath + "/1.FilterPicture";

            //폴더가 없으면 생성
            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + "/1.FilterPicture/FilterPicture.png", _byteArray);
        }
        else if(SceneManager.GetActiveScene().name.Equals("1_3_ColorProfile"))
        {
            string folderPath = Application.persistentDataPath + "/2.ColorPicture";

            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + "/2.ColorPicture/ColorPicture.png", _byteArray);
        }
        else if(SceneManager.GetActiveScene().name.Equals("3_2_MakingFrame"))
        {
            string folderPath = Application.persistentDataPath + "/3.FramePicture";

            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + "/3.FramePicture/FramePicture.png", _byteArray);
        }
        else if(SceneManager.GetActiveScene().name.Equals("1_4_UserInfoSave") ||
            SceneManager.GetActiveScene().name.Equals("2_1_FramePick") ||
            SceneManager.GetActiveScene().name.Equals("3_3_Filters"))
        {
            string folderPath = Application.persistentDataPath + "/6.Print";

            if (Directory.Exists(folderPath).Equals(false))
                Directory.CreateDirectory(folderPath);

            File.WriteAllBytes(Application.persistentDataPath + "/6.Print/PrintPicture.png", _byteArray);
        }
    }

    void UI_TakePictureShot(int _width, int _height, int _count)
    {
        pictureCount = _count;
        uiCamera.targetTexture = RenderTexture.GetTemporary(_width, _height, 64);      
        takeScereenShotOnNextFrame = true;
    }


    public static void Static_UiTakePictureShot(int _width, int _height, int _count)
    {
        instance.UI_TakePictureShot(_width, _height, _count);
    }
}
