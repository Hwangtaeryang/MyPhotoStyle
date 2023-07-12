using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }


    [SerializeField]
    private AudioSource bgmSource;

    [SerializeField]
    private AudioSource sfxSource;



    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        BGMSound();
    }

    public void BGMSound()
    {
        bgmSource.Play();
    }

    public void SFXSound(string _name)
    {
        sfxSource.PlayOneShot(Resources.Load<AudioClip>("Sound/" + _name));
    }
}
