using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject ownShip;
    [SerializeField] public float pickUpRange = 50;
    private float expSpeed = 1;
    private GameStateController controller;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ownShip = GameObject.Find("OwnShip");
        controller = GameObject.Find("GameStateController").GetComponent<GameStateController>();
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
            var expOrbs = GameObject.FindGameObjectsWithTag("exp");
            foreach (var exp in expOrbs)
            {
                exp.GetComponent<ExpBehaviour>().IsMagnetized = true;
            }
            controller.PlaySound(1);
            Destroy(gameObject);
        }
    }
}
