using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ITurret
{
    public Sprite GetSprite();

    public string GetDescription();

    public int GetLevel();

    public void LevelUp();
}