using UnityEngine;

public class BackgroundInstanceControllerComponent : MonoBehaviour
{
    public AudioSource audio;
    void Start()
    {
        var obj = GameObject.FindWithTag("music");
        if (obj != null)
            Destroy(gameObject);
        
        else
            gameObject.tag = "music";
        
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
