using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WebCamView : MonoBehaviour
{
    public RawImage mainRawImg;
    public RawImage uiRawImg;
    WebCamDevice[] webCamDevices;
    WebCamTexture webCamTexture;




    void Start()
    {
        //���� ��� ������ ī�޶� ����Ʈ
        webCamDevices = WebCamTexture.devices;

        //����� ī�޶� ����
        //���� ó�� �˻��Ǵ� �ĸ� ī�޶� ���
        //int cameraInex = -1;
        for(int i = 0; i < webCamDevices.Length; i++)
        {
            //�� �ĸ� ī�޶����� üũ
            //if(webCamDevices[i].isFrontFacing.Equals(false))
            //{ 
            //    //�ش�ī�޶� ����
            //    cameraIndex = i;
            //    break;
            //}

            //�ĸ� ī�޶� �ƴ��� üũ
            if(webCamDevices[i].isFrontFacing.Equals(true))
            {
                //���õ� ī�޶� ���� ���ο� WebcamTexture����
                webCamTexture = new WebCamTexture(webCamDevices[i].name);
                break;
            }
        }

        //ī�޶� �����϶� �¿� ������Ű��
        if(!webCamTexture.videoVerticallyMirrored)
        {
            Vector3 scaletmp = mainRawImg.GetComponent<RectTransform>().localScale;
            scaletmp.x = -1;
            if(SceneManager.GetActiveScene().name.Equals("3_1_PictureShot"))
                mainRawImg.GetComponent<RectTransform>().localScale = scaletmp;
            uiRawImg.GetComponent<RectTransform>().localScale = scaletmp;
        }

        //���ϴ� FPS����
        if(webCamTexture != null)
        {
            webCamTexture.requestedFPS = 60f;
            if (SceneManager.GetActiveScene().name.Equals("3_1_PictureShot"))
                mainRawImg.texture = webCamTexture;
            uiRawImg.texture = webCamTexture;
            webCamTexture.Play();
        }
    }

    private void OnDestroy()
    {
        //WebCamTexture���ҽ� ��ȯ
        if(webCamTexture != null)
        {
            webCamTexture.Stop();
            WebCamTexture.Destroy(webCamTexture);
        }
    }
}
