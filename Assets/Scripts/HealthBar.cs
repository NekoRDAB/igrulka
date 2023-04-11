using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform healthBar;

    [SerializeField] private GameObject ownShip;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var health = ownShip.GetComponent<Move>().health;
        healthBar.localScale = new Vector3((float)(health / 100.0), 1, 1);
    }
}
