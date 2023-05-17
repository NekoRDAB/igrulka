using UnityEngine;

public interface ITurret
{
    public Sprite GetSprite();

    public string GetDescription();

    public int GetLevel();

    public void LevelUp();

    public void Init();
}