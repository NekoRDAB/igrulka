using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using UnityEngine;

public class ExpBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject ownShip;

    [SerializeField] public float pickUpRange;
    private GameStateController gameStateController;
    private float expSpeed = 1;

    private Rigidbody2D exp;
    // Start is called before the first frame update
    void Start()
    {
        exp = GetComponent<Rigidbody2D>();
        ownShip = GameObject.Find("OwnShip");
        gameStateController = GameObject.Find("GameStateController").GetComponent<GameStateController>();
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector2.Distance(ownShip.transform.position, exp.transform.position);
        if (pickUpRange > distance)
        {
            var movement = (Vector2)(ownShip.transform.position - exp.transform.position);
            movement = Vector2.ClampMagnitude(movement, expSpeed);
            exp.MovePosition(exp.position + movement);
        }

        if (distance < Mathf.Epsilon)
        {
            gameStateController.AddExp();
            Destroy(gameObject);
            print("exp++");
        }
    }
}
