using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProtonTorpedoesTurret : MonoBehaviour, ITurret
{
    [SerializeField] private GameObject torpedo;
    public static int Damage { get; private set; }

    public static float Speed { get; private set; }
    public static int Amount { get; private set; }
    public static float CoolDown { get; private set; }

    private int level;
    public static GameObject ownShip;
    private  PlayerShipBehaviour shipBehaviour;
    private float elapsed = 0.5f;
    public float DamageMultiplier { get; set; }
    public float SpeedMultiplier { get; set; }
    public int AmountMultiplier { get; set; }
    public float CoolDownMultiplier { get; set; }

    private readonly Dictionary<int, string> DescriptionDict = new Dictionary<int, string>()
    {
        {0, "Shoot torpedoes from front of the ship"},
        {1, "Shots 1 more projectile"},
        {2, "+40 damage, +30% fire rate"},
        {3, "+20% projectile speed"},
        {4, "Shots 1 more projectile"},
    };

    private readonly Dictionary<int, Action> LevelUpDict = new Dictionary<int, Action>()
    {
        { 1, () => Amount++ },
        {
            2, () =>
            {
                Damage += 40;
                CoolDown *= 0.7f;
            }
        },
        { 3, () => Speed *= 1.2f },
        { 4, () => Amount++ },
    };

    // Start is called before the first frame update
    void Start()
    {
        ownShip = GameObject.Find("OwnShip");
        shipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        level = 0;
        Amount = 2;
        CoolDown = 4f;
        Damage = 80;
        Speed = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= CoolDown)
        {
            for (var i = 0; i < Amount; i++)
            {
                var deviation = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
                Instantiate(torpedo, ownShip.transform.position + deviation, ownShip.transform.rotation);
            }
            elapsed = 0;
        }
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
        var turret = Instantiate(gameObject, ownShip.transform.position, ownShip.transform.rotation);
        turret.transform.SetParent(ownShip.transform);
        turret.transform.localPosition = shipBehaviour.positionsList[shipBehaviour.turretsCount];
        shipBehaviour.turretsCount++;
        level++;
    }
}
