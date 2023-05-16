using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlakTurret : MonoBehaviour, ITurret
{
    [SerializeField] private GameObject FlakProjectile;
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
        {0, "Выпускает снаряды в ближайшего врага."},
        {1, "Туррель выпускает на 1 снаряд больше."},
        {2, "Радиус поражения снарядов увеличивается на 30%."},
        {3, "Туррель выпускает на 1 снаряд больше."},
        {4, "Урон от снарядов увеличивается на 15."},
    };

    private readonly Dictionary<int, Action> LevelUpDict = new Dictionary<int, Action>()
    {
        { 1, () => Amount++ },
        {
            2, () =>
            {
                CoolDown *= 0.7f;
            }
        },
        { 3, () => Amount++},
        { 4, () => Damage += 15 },
    };
    // Start is called before the first frame update
    void Start()
    {
        ownShip = GameObject.Find("OwnShip");
        shipBehaviour = ownShip.GetComponent<PlayerShipBehaviour>();
        Amount = 2;
        CoolDown = 2f;
        Damage = 50;
        Speed = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        var enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies.Length == 0)
            return;
        Transform nearestEnemy = null;
        var maxDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < maxDistance)
            {
                nearestEnemy = enemy.transform;
                maxDistance = distance;
            }
        }
        var directionToEnemy = nearestEnemy.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, directionToEnemy);
        elapsed += Time.deltaTime;
        if (elapsed >= CoolDown)
        {
            for (var i = 0; i < Amount; i++)
            {
                var deviation = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
                Instantiate(FlakProjectile, transform.position + deviation, transform.rotation);
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