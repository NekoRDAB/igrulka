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
    private static GameObject ownShip;
    private static PlayerShipBehaviour shipBehaviour;
    private float elapsed = 0.5f;
    public static GameObject ThisTurret;
    public float DamageMultiplier { get; set; }
    public float SpeedMultiplier { get; set; }
    public int AmountMultiplier { get; set; }
    public float CoolDownMultiplier { get; set; }

    private readonly Dictionary<int, string> DescriptionDict = new Dictionary<int, string>()
    {
        {0, "стреляет ракетами из носа корабля"},
        {1, "выпускает дополнительный снаряд"},
        {2, "+10 урона, +10% скорострельности"},
        {3, "+10% к скорости"},
        {4, "что - то"},
    };

    private readonly Dictionary<int, Action> LevelUpDict = new Dictionary<int, Action>()
    {
        { 0, Init },
        { 1, () => Amount++ },
        {
            2, () =>
            {
                Damage++;
                CoolDown *= 0.9f;
            }
        },
        { 3, () => Speed *= 1.1f },
        { 4, () => print("неа") },
    };

    // Start is called before the first frame update
    void Start()
    {
        ownShip = GameObject.Find("OwnShip");
        shipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        Amount = 4;
        CoolDown = 5f;
        Damage = 50;
        Speed = 30f;
        level = 1;
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
        return $"уровень {level}\n{DescriptionDict[level]}";
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

    private static void Init()
    {
        
    }
}
