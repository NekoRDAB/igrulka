using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class SuperPuperTurret : MonoBehaviour, ITurret
{
    [SerializeField] private GameObject superPuper;
    public int Damage { get; private set; }

    public float Speed { get; private set; }
    public int Amount { get; private set; }
    public float CoolDown { get; private set; }

    private int level;
    private GameObject ownShip;
    private PlayerShipBehaviour shipBehaviour;
    private float elapsed = 0.5f;
    public float DamageMultiplier { get; set; }
    public float SpeedMultiplier { get; set; }
    public int AmountMultiplier { get; set; }
    public float CoolDownMultiplier { get; set; }

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
                Instantiate(superPuper, ownShip.transform.position + deviation, ownShip.transform.rotation);
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
        return "Абоба супер пупер";
    }

    public int GetLevel()
    {
        return level;
    }

    public void LevelUp()
    {

    }

    public void Init()
    {
        
    }
}