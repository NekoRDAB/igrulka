using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ITurret
{
    public Sprite GetSprite();
    public float DamageMultiplier { get; set; }
    public float SpeedMultiplier { get; set; }

    public int AmountMultiplier { get; set; }

    public float CoolDownMultiplier { get; set; }

    public void LevelUp();
}