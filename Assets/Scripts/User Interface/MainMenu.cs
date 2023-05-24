using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{    
    [SerializeField] public KeyCode activateButtonKey = KeyCode.KeypadEnter;
    [SerializeField] public KeyCode nextButtonKey = KeyCode.UpArrow;
    [SerializeField] public KeyCode prevButtonKey = KeyCode.DownArrow;
    public Button[] buttons;
    private int activeButtonIndex;
    
    void Start()
    {
        Time.timeScale = 1;
        buttons[activeButtonIndex].Select();
    }

    void Update()
    {
        if (Input.GetKeyDown(activateButtonKey))
            buttons[activeButtonIndex].onClick.Invoke();

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

    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}