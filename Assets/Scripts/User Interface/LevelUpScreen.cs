using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Or = Unity.VisualScripting.Or;
using Random = System.Random;

public class LevelUpScreen : MonoBehaviour
{
    public Image firstCardButtonImage;
    public TextMeshProUGUI firstCardText;
    public Image secondCardButtonImage;
    public TextMeshProUGUI secondCardText;
    public Image thirdCardButtonImage;
    public TextMeshProUGUI thirdCardText;
    [SerializeField] private GameStateController controller;
    [SerializeField] private ProtonTorpedoesTurret firstTurret;
    [SerializeField] private FlakTurret secondTurret;
    [SerializeField] private RailGunTurret thirdTurret;
    [SerializeField] private MiniNukeTurret fourthTurret;
    [SerializeField] private RefrigeratorTurret fifthTurret;
    [SerializeField] private ShieldGenerator sixthTurret;

    private List<ITurret> turrets;
    private ITurret firstUpgrade;
    private ITurret secondUpgrade;
    private ITurret thirdUpgrade;


    // Start is called before the first frame update
    public void SetUp()
    {
        controller.survivalTimer.Stop();
        gameObject.SetActive(true);
        if (turrets is null)
        {
            turrets = new List<ITurret>
            {
                firstTurret,
                secondTurret,
                thirdTurret,
                fourthTurret,
                fifthTurret,
                sixthTurret
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
        var index = rnd.Next(0, availableTurrets.Count);
        var turret = availableTurrets[index];
        availableTurrets.RemoveAt(index);
        return turret;
    }

    public void BackToGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        controller.survivalTimer.Start();
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
