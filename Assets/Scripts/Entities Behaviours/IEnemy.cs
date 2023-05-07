using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    [SerializeField] public int damage { get; set; }
    public void TakeDamage(int damage);
    public void Freeze(float time);
}
