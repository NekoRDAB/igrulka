using UnityEngine;
using UnityEngine.UI;

public class ButtonsLevelUpPanel : MonoBehaviour
{    
    [SerializeField] public KeyCode activateButtonKey = KeyCode.KeypadEnter;
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
