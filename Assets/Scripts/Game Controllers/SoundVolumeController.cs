using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeController : MonoBehaviour
{
    private Slider soundVolumeSlider;
    private AudioSource audioSource;
    public AudioClip soundEffect;

    void Start()
    {
        soundVolumeSlider = GetComponent<Slider>();
        audioSource = soundVolumeSlider.GetComponent<AudioSource>();
        audioSource.clip = soundEffect;   
        soundVolumeSlider.value = PlayerPrefs.HasKey("soundVolume") ? PlayerPrefs.GetFloat("soundVolume") : 1;
    }

    public void OnValueChange()
    {
        audioSource.volume = soundVolumeSlider.value;
        audioSource.Play();
        PlayerPrefs.SetFloat("soundVolume", soundVolumeSlider.value);
    }
}