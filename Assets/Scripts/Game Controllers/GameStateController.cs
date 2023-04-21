using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public int level { get; private set; }
    public int experience { get; private set; }
    public Stopwatch survivalTimer;
    private GameObject ownShip;
    private double health;
    private AudioSource audio;
    [SerializeField] private LevelUpScreen levelUpScreen;
    [SerializeField] private GameOverScreen gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        survivalTimer = new Stopwatch();
        survivalTimer.Start();
        level = 1;
        // level = PlayerPrefs.GetInt("Level", 1); // Загружаем сохраненный уровень
        // experience = PlayerPrefs.GetInt("Experience", 0); // Загружаем сохраненный опыт
        //
        // ownShip = GameObject.Find("OwnShip");
        // var playerShipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        //
        // // Загружаем сохраненное здоровье корабля
        // var health = PlayerPrefs.GetFloat("Health", 100f);
        // playerShipBehaviour.health = health;
        //
        // var hud = FindObjectOfType<HUDController>();
        // var hudBehaviour = hud.GetComponent<HUDController>();
        //
        // var killCount = PlayerPrefs.GetString("KillCount", "0");
        // hudBehaviour.killCountText.text = killCount;

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        health = ownShip.GetComponent<PlayerShipBehaviour>().health;
        if (health <= 0 || survivalTimer.Elapsed.Minutes >= 20)
        {
            Time.timeScale = 0;
            gameOverScreen.SetUp();
        }
    }

    public void AddExp()
    {
        if (audio != null)
        {
            audio.Play();
        }
    
        experience++;
        if (experience >= level + level * 5)
        {
            level++;
            experience = 0;
            LevelUp();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void LevelUp()
    {
        // var hud = GameObject.FindObjectOfType<HUDController>();
        // var hudBehaviour = hud.GetComponent<HUDController>();
        //
        // PlayerPrefs.SetInt("Level", level);
        // PlayerPrefs.SetInt("Experience", experience);
        // PlayerPrefs.SetFloat("Health", (float)health);
        // PlayerPrefs.SetString("KillCount", hudBehaviour.killCountText.text);
        // PlayerPrefs.SetString("TimeSurvived", hudBehaviour.timeSurvived.text);
        levelUpScreen.SetUp();
        print($"Level up. New level = {level}");
        Time.timeScale = 0;
    }
}
