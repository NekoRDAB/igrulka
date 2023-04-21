using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUpScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetUp()
    {
        gameObject.SetActive(true);
    }
    
    public void BackToGame()
    {

        SceneManager.LoadScene("MainGame");
    }
}
