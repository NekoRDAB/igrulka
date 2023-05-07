using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTutorial : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    void Update() 
    {
        if (Input.anyKeyDown) 
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
