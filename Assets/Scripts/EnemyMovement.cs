using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform player;

    public Enemy enemy { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        enemy = new Enemy(10, 0.08f, GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void Update()
    {
        var movement = (Vector2)player.position - enemy.Ship.position;
        movement = Vector2.ClampMagnitude(movement, enemy.Speed);
        enemy.Ship.MovePosition(enemy.Ship.position + movement);
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(movement.x, movement.y) * 180 / Mathf.PI);
    }
}
