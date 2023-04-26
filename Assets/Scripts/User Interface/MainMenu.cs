using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button[] buttons;

    private int activeButtonIndex = 0;

    public KeyCode activateButtonKey = KeyCode.Space;
    public KeyCode nextButtonKey = KeyCode.Tab;
    public KeyCode prevButtonKey = KeyCode.LeftShift;

    void Start()
    {
        // Use the first button as the default active button
        buttons[activeButtonIndex].Select();
    }

    void Update()
    {
        if (Input.GetKeyDown(activateButtonKey))
        {
            // Trigger the button click event when the activate key is pressed
            buttons[activeButtonIndex].onClick.Invoke();
        }

        if (Input.GetKeyDown(nextButtonKey))
        {
            // Deselect the current button when the next key is pressed
            buttons[activeButtonIndex].OnDeselect(null);

            // Set the active button index to the next one
            activeButtonIndex++;

            // Wrap around to the first button if we reach the end of the list
            if (activeButtonIndex >= buttons.Length)
            {
                activeButtonIndex = 0;
            }

            // Select the new active button
            buttons[activeButtonIndex].Select();
        }

        if (Input.GetKeyDown(prevButtonKey))
        {
            // Deselect the current button when the previous key is pressed
            buttons[activeButtonIndex].OnDeselect(null);

            // Set the active button index to the previous one
            activeButtonIndex--;

            // Wrap around to the last button if we reach the beginning of the list
            if (activeButtonIndex < 0)
            {
                activeButtonIndex = buttons.Length - 1;
            }

            // Select the new active button
            buttons[activeButtonIndex].Select();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void ExitGame()
    {
        Debug.Log("Комп крашнулся, лох");
        Application.Quit();
    }
}