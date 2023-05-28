using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSuccessScreen : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI killCountText;   
    [SerializeField] KeyCode activateButtonKey = KeyCode.KeypadEnter;
    [SerializeField] public KeyCode nextButtonKey = KeyCode.UpArrow;
    [SerializeField] public KeyCode prevButtonKey = KeyCode.DownArrow;
    public Button[] buttons;
    private int activeButtonIndex = 0;
    
    void Start()
    {
        buttons[activeButtonIndex].Select();
    }
    void Update()
    {
        if (Input.GetKeyDown(activateButtonKey))
            buttons[activeButtonIndex].onClick.Invoke();

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
        killCountText.text = $"Вы отразили нашествие врагов на нашу планету и поразили {killCount} вражеских кораблей";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Menu");
    }
}