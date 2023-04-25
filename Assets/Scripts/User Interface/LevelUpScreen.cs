using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Or = Unity.VisualScripting.Or;
using Random = System.Random;

public class LevelUpScreen : MonoBehaviour
{
    public Button firstCardButton;
    public Button secondCardButton;
    public Button thirdCardButton;
    [SerializeField] private ProtonTorpedoesTurret firstTurret;
    [SerializeField] private SuperPuperTurret secondTurret;
    private List<ITurret> turrets;
    private ITurret firstUpgrade;
    private ITurret secondUpgrade;
    private ITurret thirdUpgrade;

    void Start()
    {
        turrets = new List<ITurret>();
        turrets.Add(secondTurret);
        turrets.Add(firstTurret);
        turrets.Add(firstTurret);
        ProtonTorpedoesTurret.ThisTurret = firstTurret.gameObject;
        DrawChoices();
    }

    // Start is called before the first frame update
    public void SetUp()
    {
        gameObject.SetActive(true);

    }

    private void DrawChoices()
    {
        var rnd = new Random();
        var firstIndex = rnd.Next(0, turrets.Count);
        firstUpgrade = turrets[firstIndex];
        var firstSprite = firstUpgrade.GetSprite();
        firstCardButton.image.sprite = firstSprite;
        firstCardButton.GetComponentInChildren<Text>().text = firstUpgrade.GetDescription() + " 1";

        var secondIndex = rnd.Next(0, turrets.Count);
        secondUpgrade = turrets[secondIndex];
        var secondSprite = secondUpgrade.GetSprite();
        secondCardButton.image.sprite = secondSprite;
        secondCardButton.GetComponentInChildren<Text>().text = secondUpgrade.GetDescription() + " 2";

        var thirdIndex = rnd.Next(0, turrets.Count);
        thirdUpgrade = turrets[thirdIndex];
        var thirdSprite = thirdUpgrade.GetSprite();
        thirdCardButton.image.sprite = thirdSprite;
        thirdCardButton.GetComponentInChildren<Text>().text = thirdUpgrade.GetDescription() + " 3";
    }
    
    public void BackToGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ChoseFirst()
    {
        print("1");
        firstUpgrade.LevelUp();
        BackToGame();
    }

    public void ChoseSecond()
    {
        print("2");
        secondUpgrade.LevelUp();
        BackToGame();
    }

    public void ChoseThird()
    {
        print("3");
        thirdUpgrade.LevelUp();
        BackToGame();
    }
}
