using System;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RailGunTurret : MonoBehaviour, ITurret
{
    [SerializeField] private GameObject RailGunProjectile;
    public static int Damage { get; private set; }

    public static float Speed { get; private set; }
    public static int Amount { get; private set; }
    public static float CoolDown { get; private set; }

    private int level;
    public static GameObject ownShip;
    private PlayerShipBehaviour shipBehaviour;
    private float elapsed = 0.5f;

    private readonly Dictionary<int, string> DescriptionDict = new Dictionary<int, string>()
    {
        {0, "Shots piercing projectile at random enemy"},
        {1, "+30% fire rate"},
        {2, "+30% fire rate"},
        {3, "+20 damage"},
        {4, "+40% projectile speed"},
    };

    private readonly Dictionary<int, Action> LevelUpDict = new Dictionary<int, Action>()
    {
        { 1, () => CoolDown *= 0.7f },
        { 2, () => CoolDown *= 0.7f },
        { 3, () => Damage += 20},
        { 4, () => Speed *= 1.4f },
    };
    // Start is called before the first frame update
    void Start()
    {
        ownShip = GameObject.Find("OwnShip");
        shipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        CoolDown = 5f;
        Damage = 100;
        Speed = 80f;
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
            Instantiate(RailGunProjectile, transform.position , transform.rotation);
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
