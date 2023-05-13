using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public Button[] buttons;

    private int activeButtonIndex = 0;
    [SerializeField] private GameStateController controller;
    [SerializeField] KeyCode activateButtonKey = KeyCode.KeypadEnter;
    [SerializeField] public KeyCode nextButtonKey = KeyCode.UpArrow;
    [SerializeField] public KeyCode prevButtonKey = KeyCode.DownArrow;
    
    void Start()
    {
        if (activeButtonIndex < buttons.Length && activeButtonIndex >= 0)
        {
            buttons[activeButtonIndex].Select();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(activateButtonKey))
        {
            buttons[activeButtonIndex].onClick.Invoke();
        }

        if (buttons.Length == 1)
            return;

        if (Input.GetKeyDown(nextButtonKey))
        {
            if (activeButtonIndex < 0 || activeButtonIndex >= buttons.Length) // Проверка выхода индекса за границы массива
                return;

            buttons[activeButtonIndex].OnDeselect(null);
            activeButtonIndex++;
            
            buttons[activeButtonIndex].Select();
        }

        if (Input.GetKeyDown(prevButtonKey))
        {
            if (activeButtonIndex < 0 || activeButtonIndex >= buttons.Length) // Проверка выхода индекса за границы массива
                return;

            buttons[activeButtonIndex].OnDeselect(null);
            activeButtonIndex--;
            
            buttons[activeButtonIndex].Select();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToGame();
        }
    }
    
    public void SetUp()
    {
        gameObject.SetActive(true);
    }
    
    public void BackToGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        // controller.survivalTimer.Start();
    }
    
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
