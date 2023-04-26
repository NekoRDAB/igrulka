using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsLevelUpPanel : MonoBehaviour
{
    public Button[] buttons;

    private int activeButtonIndex = 0;

    [SerializeField] public KeyCode activateButtonKey = KeyCode.KeypadEnter;
    [SerializeField] public KeyCode nextButtonKey = KeyCode.UpArrow;
    [SerializeField] public KeyCode prevButtonKey = KeyCode.DownArrow;

    void Start()
    {
        // Use the first button as the default active button
        buttons[activeButtonIndex].Select();
    }

    void Update()
    {
        if (Input.GetKeyDown(activateButtonKey))
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        if (buttons.Length == 1)
            return;

        if (Input.GetKeyDown(nextButtonKey))
        {
            buttons[activeButtonIndex].OnDeselect(null);
            
            if (activeButtonIndex == buttons.Length)
                return;
            activeButtonIndex++;
            
            buttons[activeButtonIndex].Select();
        }

        if (Input.GetKeyDown(prevButtonKey))
        {
            buttons[activeButtonIndex].OnDeselect(null);

            if (activeButtonIndex == 0)
                return;
            activeButtonIndex--;
            
            buttons[activeButtonIndex].Select();
        }
    }
}
