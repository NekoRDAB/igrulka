using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundInstanceControllerComponent : MonoBehaviour
{
    private AudioSource audio;
    void Start()
    {
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
