using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeControllerComponent : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private new AudioSource audio;
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;

    [Header("Keys")]
    [SerializeField] private string saveVolumeKey;

    [Header("Tags")]
    [SerializeField] private string sliderTag;
    [SerializeField] private string textVolumeTag;

    [Header("Parameters")]
    [SerializeField] private float volume = 0.5f;

    private void Awake()
    {
        volume = PlayerPrefs.HasKey(saveVolumeKey) ? 
            PlayerPrefs.GetFloat(saveVolumeKey) : 0.5f;
        audio.volume = volume;

        var sliderObj = GameObject.FindWithTag(sliderTag);
        if (sliderObj != null)
        {
            slider = sliderObj.GetComponent<Slider>();
            slider.value = volume;
        }
        else
        {
            PlayerPrefs.SetFloat(saveVolumeKey, volume);
        }
    }

    private void LateUpdate()
    {
        slider = GameObject.FindWithTag(sliderTag)?.GetComponent<Slider>();
        if (slider == null)
        {
            return;
        }
        
        volume = slider.value;
        if (audio.volume != volume)
        {
            PlayerPrefs.SetFloat(saveVolumeKey, volume);
        }

        text = GameObject.FindWithTag(textVolumeTag)?.GetComponent<Text>();
        if (text != null)
        {
            text.text = Mathf.Round(volume * 100) + "%";
        }

        audio.volume = volume;
    }
}