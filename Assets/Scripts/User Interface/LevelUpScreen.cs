using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

public class LevelUpScreen : MonoBehaviour
{    
    [SerializeField] private PlayerShipBehaviour ownShip;
    [SerializeField] private GameStateController controller;
    [SerializeField] private ProtonTorpedoesTurret protonTorpedoesTurret;
    [SerializeField] private FlakTurret flakTurret;
    [SerializeField] private RailGunTurret railGunTurret;
    [SerializeField] private MiniNukeTurret miniNukeTurret;
    [SerializeField] private RefrigeratorTurret refrigeratorTurret;
    [SerializeField] private ShieldGenerator shieldGenerator;
    [SerializeField] private LaserTurretBehaviour laserTurret;
    public Image firstCardButtonImage;
    public TextMeshProUGUI firstCardText;
    public Image secondCardButtonImage;
    public TextMeshProUGUI secondCardText;
    public Image thirdCardButtonImage;
    public TextMeshProUGUI thirdCardText;
    public List<ITurret> turrets;
    private ITurret firstUpgrade;
    private ITurret secondUpgrade;
    private ITurret thirdUpgrade;
    private bool isInitialization = true;
    
    public void SetUp()
    {
        gameObject.SetActive(true);
        if (turrets is null)
        {
            turrets = new List<ITurret>
            {
                protonTorpedoesTurret,
                flakTurret,
                railGunTurret,
                miniNukeTurret,
                refrigeratorTurret,
                shieldGenerator,
                laserTurret
            };
        }
        DrawChoices();
    }

    private void DrawChoices()
    {
        var rnd = new Random();
        var availableTurrets = turrets
            .Where(t => t.GetLevel() < 5)
            .ToList();
        if (ownShip.turretsCount == 5)
        {
            availableTurrets = availableTurrets
                .Where(t => t.GetLevel() > 0)
                .ToList();
        }
        

        firstUpgrade = GetRandomTurret(rnd, availableTurrets);
        secondUpgrade = GetRandomTurret(rnd, availableTurrets);
        thirdUpgrade = GetRandomTurret(rnd, availableTurrets);

        firstCardButtonImage.sprite = firstUpgrade.GetSprite();
        secondCardButtonImage.sprite = secondUpgrade.GetSprite();
        thirdCardButtonImage.sprite = thirdUpgrade.GetSprite();

        firstCardText.text = firstUpgrade.GetDescription();
        secondCardText.text = secondUpgrade.GetDescription();
        thirdCardText.text = thirdUpgrade.GetDescription();
    } 

    private ITurret GetRandomTurret(Random rnd, List<ITurret> availableTurrets)
    {
        if (isInitialization)
        {
            var initializeTurret = InitializeFirstTurret(rnd, availableTurrets);
            return initializeTurret;
        }
        var index = rnd.Next(0, availableTurrets.Count);
        var turret = availableTurrets[index];
        availableTurrets.RemoveAt(index);
        return turret;
    }
    
    private ITurret InitializeFirstTurret(Random rnd, List<ITurret> availableTurrets)
    {
        availableTurrets.Remove(refrigeratorTurret);
        availableTurrets.Remove(shieldGenerator);
        var index = rnd.Next(0, availableTurrets.Count);
        var turret = availableTurrets[index];
        availableTurrets.RemoveAt(index);
        return turret;
    }

    public void BackToGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ChoseFirst()
    {
        print("1");
        if (firstUpgrade.GetLevel() == 0)
            firstUpgrade.Init();
        
        else
            firstUpgrade.LevelUp();
        
        BackToGame();
    }

    public void ChoseSecond()
    {
        print("2");
        if (secondUpgrade.GetLevel() == 0)
            secondUpgrade.Init();
        
        else
            secondUpgrade.LevelUp();
        
        BackToGame();
    }

    public void ChoseThird()
    {
        print("3");
        if (thirdUpgrade.GetLevel() == 0)
            thirdUpgrade.Init();
        
        else
            thirdUpgrade.LevelUp();
        
        BackToGame();
    }
}
