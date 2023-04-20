using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtonTorpedoesTurret : MonoBehaviour, ITurret
{
    public int Damage { get; private set; }

    public float Speed { get; private set; }
    public int Amount { get; private set; }
    public float CoolDown { get; private set; }

    private int level;

    public float DamageMultiplier { get; set; }
    public float SpeedMultiplier { get; set; }
    public int AmountMultiplier { get; set; }
    public float CoolDownMultiplier { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetSprite()
    {
        return gameObject.GetComponent<Sprite>();
    }
    public void LevelUp()
    {
        throw new System.NotImplementedException();
    }
}
