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
        survivalTimer = new Stopwatch();
        survivalTimer.Start();
        level = 1;
        ownShip = GameObject.Find("OwnShip");
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        health = ownShip.GetComponent<PlayerShipBehaviour>().health;
        if (health <= 0 || survivalTimer.Elapsed.Minutes >= 20)
        {
            gameOverScreen.SetUp(hudController.killCount);
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            survivalTimer.Stop();
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
