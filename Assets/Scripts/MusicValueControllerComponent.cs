using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeControllerComponent : MonoBehaviour
{
    private Slider musicVolumeSlider;

    void Start()
    {
        musicVolumeSlider = GetComponent<Slider>();
        musicVolumeSlider.value = PlayerPrefs.HasKey("musicVolume") ? PlayerPrefs.GetFloat("musicVolume") : 1;
    }
    
    public void OnValueChange()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
    }
}