using System;
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

    private readonly Dictionary<int, string> DescriptionDict = new()
    {
        {0, "Выпускает торпеды из носа корабля."},
        {1, "Туррель выпускает на 1 снаряд больше."},
        {2, "Урон от снарядов увеличивается на 40. Скорострельность турели увеличивается на 30%."},
        {3, "Скорость снарядов увеличивается на 20%."},
        {4, "Туррель выпускает на 1 снаряд больше."},
    };

    private readonly Dictionary<int, Action> LevelUpDict = new()
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
    
    void Start()
    {
        ownShip = GameObject.Find("OwnShip");
        shipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        Amount = 2;
        CoolDown = 2f;
        Damage = 80;
        Speed = 30f;
    }
    
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
        return $"Уровень {level}\n{DescriptionDict[level]}";
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
