using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInterface : Component
{
    public readonly int Damage;
    public readonly float Speed;
    public readonly Rigidbody2D Ship;
    public EnemyInterface(int damage, float speed, Rigidbody2D ship)
    {
        Damage = damage;
        Speed = speed;
        Ship = ship;
    }
}
