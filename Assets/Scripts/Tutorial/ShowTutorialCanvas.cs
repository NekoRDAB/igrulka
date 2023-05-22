using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour 
{
    [SerializeField] private GameObject prevCanvas; 
    [SerializeField] private GameObject currentCanvas; 
    [SerializeField] private GameObject nextCanvas;
    [SerializeField] private KeyCode nextButtonKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode prevButtonKey = KeyCode.RightArrow;
    
    private void Start() 
    {
        currentCanvas.SetActive(true); // открываем канвас после перехода на сцену
    }

    private void Update() 
    {
        if (Input.GetKeyDown(prevButtonKey))
        {
            if (prevCanvas != null)
            {
                prevCanvas.SetActive(true);
                currentCanvas.SetActive(false);
            }
        }
        if (Input.GetKeyDown(nextButtonKey)) 
        {
            if (nextCanvas != null)
            {
                nextCanvas.SetActive(true);
                currentCanvas.SetActive(false);
            }
            else
                SceneManager.LoadScene("Menu");
        }
    }
}