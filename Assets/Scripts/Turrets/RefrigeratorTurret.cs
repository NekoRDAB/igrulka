using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class RefrigeratorTurret : MonoBehaviour, ITurret
{
    [SerializeField] private GameObject fridge;
    public static float Speed { get; private set; }
    public static int Amount { get; private set; }
    public static float CoolDown { get; private set; }

    private int level;
    public static GameObject ownShip;
    private PlayerShipBehaviour shipBehaviour;
    private float elapsed = 0.5f;
    public float SpeedMultiplier { get; set; }
    public int AmountMultiplier { get; set; }
    public float CoolDownMultiplier { get; set; }

    private readonly Dictionary<int, string> DescriptionDict = new Dictionary<int, string>()
    {
        {0, "Выпускает снаряд, замораживающий случайного врага."},
        {1, "Туррель выпускает на 1 снаряд больше."},
        {2, "Время перезарядки снарядов уменьшается на 30%."},
        {3, "Туррель выпускает на 2 снаряда больше."},
        {4, "Время перезарядки снарядов уменьшается на 50%."},
    };

    private readonly Dictionary<int, Action> LevelUpDict = new Dictionary<int, Action>()
    {
        { 1, () => Amount++ },
        { 2, () => CoolDown *= 0.7f},
        { 3, () => Amount += 2 },
        { 4, () => CoolDown *= 0.5f },
    };

    // Start is called before the first frame update
    void Start()
    {
        ownShip = GameObject.Find("OwnShip");
        shipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        Amount = 1;
        CoolDown = 4f;
        Speed = 40f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= CoolDown)
        {
            for (var i = 0; i < Amount; i++)
            {
                var enemies = GameObject.FindGameObjectsWithTag("enemy");
                var enemy = enemies[Random.Range(0, enemies.Length)];
                var directionToEnemy = enemy.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, directionToEnemy);
                Instantiate(fridge, transform.position, transform.rotation);
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
