using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour 
{
    [SerializeField] private GameObject prevCanvas; 
    [SerializeField] private GameObject currentCanvas; 
    [SerializeField] private GameObject nextCanvas;
    [SerializeField] private KeyCode nextCanvasKey = KeyCode.RightArrow;
    [SerializeField] private MouseButton secondaryNextCanvasKey = MouseButton.Left;
    [SerializeField] private KeyCode prevCanvasKey = KeyCode.LeftArrow;
    
    private void Start() 
    {
        currentCanvas.SetActive(true); // открываем канвас после перехода на сцену
    }

    private void Update() 
    {
        if (Input.GetKeyDown(prevCanvasKey))
        {
            if (prevCanvas != null)
            {
                prevCanvas.SetActive(true);
                currentCanvas.SetActive(false);
            }
        }
        if (Input.GetKeyDown(nextCanvasKey) || Input.GetMouseButtonDown((int)secondaryNextCanvasKey)) 
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