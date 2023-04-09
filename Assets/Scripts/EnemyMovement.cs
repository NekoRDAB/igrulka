using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Rigidbody2D ship;

    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement = (Vector2)player.position - ship.position;
        movement = Vector2.ClampMagnitude(movement, speed);
        ship.MovePosition(ship.position + movement);
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(movement.x, movement.y) * 180 / Mathf.PI);
    }
}
