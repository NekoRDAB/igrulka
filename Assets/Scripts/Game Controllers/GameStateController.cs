using Assets.Scripts;
using UnityEngine;

public class GameStateController : MonoBehaviour
{    
    [SerializeField] private HUDController hudController;
    [SerializeField] private LevelUpScreen levelUpScreen;
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private GameOverSuccessScreen gameOverSuccessScreen;
    [SerializeField] private PauseScreen pauseScreen;
    public int level { get; private set; }
    public int experience { get; private set; }
    private GameObject ownShip;
    private double health;
    private AudioSource audio;
    public float survivalTimeLimit; 
    public float survivalTimer;

    
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
    
    void Update()
    {
        health = ownShip.GetComponent<PlayerShipBehaviour>().health;
        
        survivalTimer -= Time.deltaTime;
        if (survivalTimer <= 0f)
        {
            gameOverSuccessScreen.SetUp(hudController.killCount);
            Time.timeScale = 0;
        }

        if (health <= 0)
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
        if (experience >= level * level)
        {
            level++;
            experience = 0;
            if (level <= 25)
                LevelUp();
        }
    }
    
    public void LevelUp()
    {
        levelUpScreen.SetUp();
        Time.timeScale = 0;
    }

    public void PlaySound(float pitch)
    {
        audio.pitch = pitch;
        audio.volume = PlayerPrefs.GetFloat("soundVolume");
        audio.Play();
    }
}
