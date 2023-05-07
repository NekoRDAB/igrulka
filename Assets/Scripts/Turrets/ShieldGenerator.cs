using System;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShieldGenerator : MonoBehaviour, ITurret
{
    [SerializeField] private GameObject ShieldBubble;
    public static int Damage { get; private set; }

    public static int ShieldHealth { get; private set; }

    public static float Speed { get; private set; }
    public static int Amount { get; private set; }
    public static float CoolDown { get; private set; }

    private int level;
    public static GameObject ownShip;
    private PlayerShipBehaviour shipBehaviour;
    private float elapsed = 0.5f;

    private readonly Dictionary<int, string> DescriptionDict = new Dictionary<int, string>()
    {
        {0, "Generates shield around ship"},
        {1, " +30 shield health"},
        {2, "-30% shield recharging time"},
        {3, "+30 shield health"},
        {4, "-30% shield recharging time"},
    };

    private readonly Dictionary<int, Action> LevelUpDict = new Dictionary<int, Action>()
    {
        { 1, () => ShieldHealth += 30 },
        { 2, () => CoolDown *= 0.7f },
        { 3, () => ShieldHealth += 30},
        { 4, () => CoolDown *= 0.7f },
    };
    // Start is called before the first frame update
    void Start()
    {
        ownShip = GameObject.Find("OwnShip");
        shipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        Amount = 2;
        CoolDown = 10f;
        Damage = 45;
        Speed = 30f;
        ShieldHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetSprite()
    {
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public string GetDescription()
    {
        return $"level {level}\n{DescriptionDict[level]}";
    }

    public int GetLevel()
    {
        return level;
    }

    public void LevelUp()
    {
        LevelUpDict[level].Invoke();
        level++;
    }

    public void Init()
    {
        Start();
        var shield = Instantiate(ShieldBubble, ownShip.transform.position, ownShip.transform.rotation);
        shield.transform.SetParent(ownShip.transform);
        var turret = Instantiate(gameObject, ownShip.transform.position, ownShip.transform.rotation);
        turret.transform.SetParent(ownShip.transform);
        turret.transform.localPosition = shipBehaviour.positionsList[shipBehaviour.turretsCount];
        shipBehaviour.turretsCount++;
        level++;
    }
}
