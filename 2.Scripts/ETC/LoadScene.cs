using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static LoadScene instance { get; private set; }


    public GameObject loaderBack;
    public Image timerRot;

    private AsyncOperation op;

    [SerializeField]
    private Slider sliderBar;



    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    public void LoadByName(string _loadSceneName)
    {
        GameManager.instance.SFXSound("ClickSound");

        timerRot.transform.DORotate(new Vector3(0f, 0f, -360f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease
            .Linear).SetLoops(-1);
        loaderBack.SetActive(true);
        StartCoroutine(_LoadScene(_loadSceneName));
    }

    public void FilterScene()
    {
        GameManager.instance.SFXSound("ClickSound");

        timerRot.transform.DORotate(new Vector3(0f, 0f, -360f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease
            .Linear).SetLoops(-1);
        loaderBack.SetActive(true);

        if(PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Profile"))
            StartCoroutine(_LoadScene("1_3_ColorProfile"));
        else if (PlayerPrefs.GetString("MyPhotoStyle_PictureMode").Equals("Celebrity"))
            StartCoroutine(_LoadScene("2_1_FramePick"));
    }


    IEnumerator _LoadScene(string _nextScene)
    {
        yield return null;
        op = SceneManager.LoadSceneAsync(_nextScene);

        op.allowSceneActivation = false;

        float timer = 0f;

        while(!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime * 0.01f;

            if(op.progress < 0.9f)
            {
                sliderBar.value = Mathf.Lerp(sliderBar.value, op.progress, timer);
                if (sliderBar.value >= op.progress)
                    timer = 0;
            }
            else
            {
                sliderBar.value = Mathf.Lerp(sliderBar.value, 1f, timer);
                if(sliderBar.value >= 0.99f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
