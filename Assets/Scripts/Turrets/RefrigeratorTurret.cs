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
        {0, "Freezes one random enemy"},
        {1, "Gains +1 amount"},
        {2, "Cooldown is decreased"},
        {3, "Gains +2 amount"},
        {4, "Cooldown is decreased"},
    };

    private readonly Dictionary<int, Action> LevelUpDict = new Dictionary<int, Action>()
    {
        { 1, () => Amount++ },
        { 2, () => CoolDown -= 1.5f},
        { 3, () => Amount += 2 },
        { 4, () => CoolDown -= 2f },
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
