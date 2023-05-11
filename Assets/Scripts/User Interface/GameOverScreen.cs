using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Button[] buttons;
    [SerializeField] private TextMeshProUGUI killCountText;
    private int activeButtonIndex = 0;

    [SerializeField] KeyCode activateButtonKey = KeyCode.KeypadEnter;
    [SerializeField] public KeyCode nextButtonKey = KeyCode.UpArrow;
    [SerializeField] public KeyCode prevButtonKey = KeyCode.DownArrow;
    void Start()
    {
        buttons[activeButtonIndex].Select();
    }
    void Update()
    {
        if (Input.GetKeyDown(activateButtonKey))
        {
            buttons[activeButtonIndex].onClick.Invoke();
        }

        if (Input.GetKeyDown(nextButtonKey))
        {
            if (activeButtonIndex == buttons.Length - 1)
                return;
            buttons[activeButtonIndex].OnDeselect(null);
            
            activeButtonIndex++;
            
            buttons[activeButtonIndex].Select();
        }

        if (Input.GetKeyDown(prevButtonKey))
        {
            if (activeButtonIndex == 0) 
                return;
            buttons[activeButtonIndex].OnDeselect(null);
            
            activeButtonIndex--;
            
            buttons[activeButtonIndex].Select();
        }
    }
    public void SetUp(int killCount)
    {
        gameObject.SetActive(true);
        Start();
        killCountText.text = $"Enemies destroyed: {killCount}";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
