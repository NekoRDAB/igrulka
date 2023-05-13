using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public int level { get; private set; }
    public int experience { get; private set; }
    // public Stopwatch survivalTimer;
    private GameObject ownShip;
    private double health;
    private AudioSource audio;
    public float survivalTimeLimit = 1200f; 
    public float survivalTimer;
    [SerializeField]private HUDController hudController;
    [SerializeField] private LevelUpScreen levelUpScreen;
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private PauseScreen pauseScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("soundVolume"))
            PlayerPrefs.SetFloat("soundVolume", 1);
        Time.timeScale = 1;
        level = 1;
        ownShip = GameObject.Find("OwnShip");
        audio = GetComponent<AudioSource>();
        LevelUp();
        survivalTimer = survivalTimeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        health = ownShip.GetComponent<PlayerShipBehaviour>().health;

        // уменьшаем значение таймера каждый кадр
        survivalTimer -= Time.deltaTime;

        // если таймер достиг нуля или здоровье игрока меньше или равно нулю,
        // вызываем экран проигрыша и останавливаем игру
        if (survivalTimer <= 0f || health <= 0)
        {
            gameOverScreen.SetUp(hudController.killCount);
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseScreen.SetUp();
        }
    }

    public void AddExp()
    {
        PlaySound(1);

        experience++;
        if (experience >= level * 4)
        {
            level++;
            experience = 0;
            if (level < 25)
                LevelUp();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void LevelUp()
    {
        levelUpScreen.SetUp();
        print($"Level up. New level = {level}");
        Time.timeScale = 0;
    }

    public void PlaySound(float pitch)
    {
        audio.pitch = pitch;
        audio.volume = PlayerPrefs.GetFloat("soundVolume");
        audio.Play();
    }
}
