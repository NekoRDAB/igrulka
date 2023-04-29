using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Microsoft.Unity.VisualStudio.Editor;
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
    [SerializeField] private ProtonTorpedoesTurret firstTurret;
    [SerializeField] private FlakTurret secondTurret;
    [SerializeField] private SuperDuperTurret thirdTurret;

    private List<ITurret> turrets;
    private ITurret firstUpgrade;
    private ITurret secondUpgrade;
    private ITurret thirdUpgrade;
    

    // Start is called before the first frame update
    public void SetUp()
    {
        gameObject.SetActive(true);
        if (turrets is null)
        {
            turrets = new List<ITurret>();
            turrets.Add(firstTurret);
            turrets.Add(secondTurret);
            turrets.Add(thirdTurret);
        }
        DrawChoices();
    }

    private void DrawChoices()
    {
        var rnd = new Random();
        var availableTurrets = new List<ITurret>(turrets);
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
