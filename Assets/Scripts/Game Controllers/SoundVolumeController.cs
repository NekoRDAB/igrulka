using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeController : MonoBehaviour
{
    private Slider soundVolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
       soundVolumeSlider = GetComponent<Slider>();
       soundVolumeSlider.value = PlayerPrefs.HasKey("soundVolume") ? PlayerPrefs.GetFloat("soundVolume") : 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValueChange()
    {
        PlayerPrefs.SetFloat("soundVolume", soundVolumeSlider.value);
    }
}
