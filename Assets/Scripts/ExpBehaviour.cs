using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using UnityEngine;

public class ExpBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject ownShip;

    [SerializeField] public float pickUpRange;
    private float expSpeed = 1;

    private Rigidbody2D exp;
    // Start is called before the first frame update
    void Start()
    {
        exp = GetComponent<Rigidbody2D>();
        ownShip = GameObject.Find("OwnShip");
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
            //AddExp();
            var player = ownShip.GetComponent<PlayerShipBehaviour>();
            player.expa++;
            Destroy(gameObject);
            print("exp++");
        }
    }
}
