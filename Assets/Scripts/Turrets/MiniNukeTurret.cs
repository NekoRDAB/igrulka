using System;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniNukeTurret : MonoBehaviour, ITurret
{
    [SerializeField] private GameObject MiniNukeProjectile;
    public static int Damage { get; private set; }

    public static float Speed { get; private set; }
    public static float CoolDown { get; private set; }

    public static float Duration { get; private set; }

    private int level;
    public static GameObject ownShip;
    private PlayerShipBehaviour shipBehaviour;
    private float elapsed = 0.5f;

    private readonly Dictionary<int, string> DescriptionDict = new()
    {
        {0, "Выпускает ядерный снаряд в ближайшего врага."},
        {1, "Скорострельность турели увеличивается на 30%."},
        {2, "Продолжительность ядерного взрыва увеличивается на 30%."},
        {3, "Урон от снаряда увеличивается на 20."},
        {4, "Скорость снаряда увеличивается на 40%."},
    };

    private readonly Dictionary<int, Action> LevelUpDict = new()
    {
        { 1, () => CoolDown *= 0.7f },
        { 2, () => Duration *= 1.3f },
        { 3, () => Damage += 20},
        { 4, () => Speed *= 1.4f },
    };
    // Start is called before the first frame update
    void Start()
    {
        ownShip = GameObject.Find("OwnShip");
        shipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        CoolDown = 6f;
        Damage = 100;
        Speed = 30f;
        Duration = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        var enemy = GameObject.FindGameObjectWithTag("enemy");

        var directionToEnemy = enemy.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, directionToEnemy);
        elapsed += Time.deltaTime;
        if (elapsed >= CoolDown)
        {
            Instantiate(MiniNukeProjectile, transform.position, transform.rotation);
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
