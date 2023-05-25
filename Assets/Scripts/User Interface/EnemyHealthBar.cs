using System;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject ownShip;
    [SerializeField] private Transform healthBar;

    void Start()
    {

    }

    void Update()
    {
        try
        {
            var shipPosition = ownShip.transform.position;
            transform.position = new Vector3(shipPosition.x, shipPosition.y - 22, 0);
            var health = ownShip.GetComponent<MiniBossBehaviour>().health;
            healthBar.localScale = new Vector3((float)(health / 1500.0), 1, 1);
        }
        catch (MissingReferenceException e)
        {
            Destroy(gameObject);
        }
    }
}
