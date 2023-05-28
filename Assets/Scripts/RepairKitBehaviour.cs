using System;
using Assets.Scripts;
using UnityEngine;

public class RepairKitBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject ownShip;
    [SerializeField] private GameObject damageNumbers;
    [SerializeField] public float pickUpRange = 50;
    private float expSpeed = 1;
    private GameStateController controller;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ownShip = GameObject.Find("OwnShip");
        controller = GameObject.Find("GameStateController").GetComponent<GameStateController>();
    }
    
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
            var player = ownShip.GetComponent<PlayerShipBehaviour>();
            player.health = Math.Min(300, player.health + 50);
            var damageNumber = Instantiate(damageNumbers, ownShip.transform.position, Quaternion.identity);
            damageNumber.GetComponent<NumbersBehaviour>().SetNumber(25, regen:true);
            controller.PlaySound(1.5f);
            Destroy(gameObject);
        }
    }
}
