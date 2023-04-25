using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Microsoft.Unity.VisualStudio.Editor;
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

    void Start()
    {
        var turrets = new List<ITurret>();
        turrets.Add(secondTurret);
        turrets.Add(firstTurret);
        turrets.Add(firstTurret);

        var rnd = new Random();

        var firstIndex = rnd.Next(0, turrets.Count);
        var firstUpgrade = turrets[firstIndex];
        var firstSprite = firstUpgrade.GetSprite();
        firstCardButton.image.sprite = firstSprite;
        firstCardButton.GetComponentInChildren<Text>().text = firstUpgrade.GetDescription() + " 1";
        
        var secondIndex = rnd.Next(0, turrets.Count);
        var secondUpgrade = turrets[secondIndex];
        var secondSprite = secondUpgrade.GetSprite();
        secondCardButton.image.sprite = secondSprite;
        secondCardButton.GetComponentInChildren<Text>().text = secondUpgrade.GetDescription() + " 2";
        
        var thirdIndex = rnd.Next(0, turrets.Count);
        var thirdUpgrade = turrets[thirdIndex];
        var thirdSprite = thirdUpgrade.GetSprite();
        thirdCardButton.image.sprite = thirdSprite;
        thirdCardButton.GetComponentInChildren<Text>().text = thirdUpgrade.GetDescription() + " 3";

    }

    // Start is called before the first frame update
    public void SetUp()
    {
        gameObject.SetActive(true);
    }
    
    public void BackToGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
