using System;
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : MonoBehaviour, ITurret
{
    [SerializeField] private GameObject ShieldBubble;
    public static int Damage { get; private set; }
    public static int ShieldHealth { get; private set; }
    public static float CoolDown { get; private set; }
    private int level;
    public static GameObject ownShip;
    private PlayerShipBehaviour shipBehaviour;

    private readonly Dictionary<int, string> DescriptionDict = new()
    {
        {0, "Создаёт щит вокруг корабля."},
        {1, "Прочность щита увеличивается на 30."},
        {2, "Время перезарядки щита уменьшается на 30%."},
        {3, "Прочность щита увеличивается на 30."},
        {4, "Время перезарядки щита уменьшается на 30%."},
    };

    private readonly Dictionary<int, Action> LevelUpDict = new()
    {
        { 1, () => ShieldHealth += 30 },
        { 2, () => CoolDown *= 0.7f },
        { 3, () => ShieldHealth += 30},
        { 4, () => CoolDown *= 0.7f },
    };

    void Start()
    {
        ownShip = GameObject.Find("OwnShip");
        shipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        CoolDown = 10f;
        Damage = 45;
        ShieldHealth = 100;
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
        var shield = Instantiate(ShieldBubble, ownShip.transform.position, ownShip.transform.rotation);
        shield.transform.SetParent(ownShip.transform);
        var turret = Instantiate(gameObject, ownShip.transform.position, ownShip.transform.rotation);
        turret.transform.SetParent(ownShip.transform);
        turret.transform.localPosition = shipBehaviour.positionsList[shipBehaviour.turretsCount];
        shipBehaviour.turretsCount++;
        level++;
    }
}
