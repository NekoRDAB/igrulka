using System;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

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
        {0, "Выпускает ядерный снаряд в случайного врага"},
        {1, "Скорострельность турели увеличивается на 15%"},
        {2, "Продолжительность ядерного взрыва увеличивается на 20%"},
        {3, "Урон от снаряда увеличивается на 20"},
        {4, "Скорость снаряда увеличивается на 20%"},
        {5, "Продолжительность ядерного взрыва увеличивается на 30%"},
        {6, "Скорострельность турели увеличивается на 20%"}
    };

    private readonly Dictionary<int, Action> LevelUpDict = new()
    {
        { 1, () => CoolDown *= 0.85f },
        { 2, () => Duration *= 1.2f },
        { 3, () => Damage += 20},
        { 4, () => Speed *= 1.2f },
        { 5, () => Duration *= 1.3f},
        { 6, () => CoolDown *= 0.8f}
    };

    void Start()
    {
        ownShip = GameObject.Find("OwnShip");
        shipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        CoolDown = 6f;
        Damage = 100;
        Speed = 30f;
        Duration = 1.5f;
    }
    
    void Update()
    {
        var enemy = GameObject.FindGameObjectWithTag("enemy");
        if (enemy == null)
            return;

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
        return $"Ядерная пушка. Уровень {level}\n{DescriptionDict[level]}";
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
