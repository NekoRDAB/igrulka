using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeControllerComponent : MonoBehaviour
{
    private Slider musicVolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        musicVolumeSlider = GetComponent<Slider>();
        musicVolumeSlider.value = PlayerPrefs.HasKey("musicVolume") ? PlayerPrefs.GetFloat("musicVolume") : 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnValueChange()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
    }
}