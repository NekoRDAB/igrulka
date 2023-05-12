using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundInstanceControllerComponent : MonoBehaviour
{
    private AudioSource audio;
    void Start()
    {
        var obj = GameObject.FindWithTag("music");
        if (obj != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.tag = "music";
        }
        DontDestroyOnLoad(gameObject);
        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetFloat("musicVolume", 1);
        audio = GetComponent<AudioSource>();
        audio.Play();
        audio.volume = PlayerPrefs.GetFloat("musicVolume");
    }

    void FixedUpdate()
    {
        audio.volume = PlayerPrefs.GetFloat("musicVolume");
    }
}
