using UnityEngine;
using UnityEngine.SceneManagement;

public class StopBackgroundMusicOnTutorial : MonoBehaviour
{
    private BackgroundInstanceControllerComponent backgroundMusic;

    private void Start()
    {
        backgroundMusic = FindObjectOfType<BackgroundInstanceControllerComponent>();

        if (backgroundMusic != null && SceneManager.GetActiveScene().name == "TutorialScene")
        {
            backgroundMusic.audio.Stop();
        }
    }
    
    private void OnDestroy()
    {
        if (backgroundMusic != null && SceneManager.GetActiveScene().name != "Tutorial")
        {
            backgroundMusic.audio.Play();
        }
    }
}