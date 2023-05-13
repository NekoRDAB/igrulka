using System;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretBehaviour : MonoBehaviour, ITurret
{
    public static int Damage { get; private set; }

    public static float Speed { get; private set; }
    public static int Amount { get; private set; }
    public static float CoolDown { get; private set; }

    private int level;
    public static GameObject ownShip;
    private PlayerShipBehaviour shipBehaviour;

    private readonly Dictionary<int, string> DescriptionDict = new Dictionary<int, string>()
    {
        {0, "Излучает лазер пронизывающий врагов"},
        {1, "+50% скорости"},
        {2, "+25 урону"},
        {3, "+50% скорости"},
        {4, "+25 урону"},
    };

    private readonly Dictionary<int, Action> LevelUpDict = new Dictionary<int, Action>()
    {
        { 1, () => Speed += 10f},
        { 2, () => Damage += 25 },
        { 3, () => Speed += 10f },
        { 4, () => Damage += 25 },
    };
    // Start is called before the first frame update
    void Start()
    {
        ownShip = GameObject.Find("OwnShip");
        shipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        Damage = 40;
        Speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0,transform.rotation.eulerAngles.z + Speed * Time.deltaTime);
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
