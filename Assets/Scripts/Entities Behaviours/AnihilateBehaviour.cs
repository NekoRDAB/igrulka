using System;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnihilateBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject ownShip;
    [SerializeField] public float pickUpRange = 50;
    private float expSpeed = 1;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ownShip = GameObject.Find("OwnShip");
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector2.Distance(ownShip.transform.position, rb.transform.position);
        if (pickUpRange > distance)
        {
            var movement = (Vector2)(ownShip.transform.position - rb.transform.position);
            movement = Vector2.ClampMagnitude(movement, expSpeed);
            rb.MovePosition(rb.position + movement);
        }

        if (distance < 5)
        {
            var enemies = GameObject.FindGameObjectsWithTag("enemy");
            foreach (var enemy in enemies)
            {
                var ienemy = enemy.GetComponent<IEnemy>();
                if (ienemy.damage != 0)
                    ienemy.TakeDamage(9999);
            }
            Destroy(gameObject);
        }
    }
}
