using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private Slider slider;
    private TextMeshProUGUI level;
    private Canvas HUD;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI timeSurvived;
    private GameStateController gameStateController;
    public int killCount;

    void Start()
    {
        HUD = GetComponent<Canvas>();
        timeSurvived = GameObject.Find("TimeSurvived").GetComponent<TextMeshProUGUI>();
        slider = GameObject.Find("ExpBar").GetComponent<Slider>();
        level = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        killCountText = GameObject.Find("KillCount").GetComponent<TextMeshProUGUI>();
        gameStateController = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        slider.minValue = 0;
    }
    
    void Update()
    {
        slider.maxValue = gameStateController.level * gameStateController.level * 3;
        slider.value = gameStateController.experience;
        level.text = $"Уровень: {gameStateController.level}";
        killCountText.text = $"Убийств: {killCount}";
        var timer = gameStateController.survivalTimer;
        var maxTime = gameStateController.survivalTimeLimit;
        var timeSpan = new System.TimeSpan(0, 0, (int)(maxTime - timer));
        timeSurvived.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }

    public void IncreaseKillCount()
    {
        killCount++;
    }
}
