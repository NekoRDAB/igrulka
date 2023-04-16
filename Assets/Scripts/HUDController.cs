using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private Slider slider;
    private TextMeshProUGUI level;
    private Canvas HUD;
    private TextMeshProUGUI killCountText;
    private GameStateController gameStateController;

    private int killCount;
    // Start is called before the first frame update
    void Start()
    {
        HUD = GetComponent<Canvas>();
        slider = GameObject.Find("ExpBar").GetComponent<Slider>();
        level = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        killCountText = GameObject.Find("KillCount").GetComponent<TextMeshProUGUI>();
        gameStateController = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        slider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = gameStateController.level + gameStateController.level * 5;
        slider.value = gameStateController.experience;
        level.text = $"lvl. {gameStateController.level}";
        killCountText.text = $"killed: {killCount}";
    }

    public void IncreaseKillCount()
    {
        killCount++;
    }
}
