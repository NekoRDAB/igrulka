using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Button[] buttons;

    private int activeButtonIndex = 0;

    public TextMeshProUGUI killsCountText;
    public HUDController hud;

    [SerializeField] KeyCode activateButtonKey = KeyCode.KeypadEnter;
    [SerializeField] public KeyCode nextButtonKey = KeyCode.UpArrow;
    [SerializeField] public KeyCode prevButtonKey = KeyCode.DownArrow;
    void Start()
    {
        var hud = gameObject.AddComponent<HUDController>();
        killsCountText.text = $"You killed {hud.killCount} enemy";
        buttons[activeButtonIndex].Select();
    }
    void Update()
    {
        killsCountText.text = $"You killed {hud.killCount} enemy";
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
    
    public void SetUp()
    {
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
