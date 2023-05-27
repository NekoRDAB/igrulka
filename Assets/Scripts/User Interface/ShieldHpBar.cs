using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHpBar : MonoBehaviour
{
    [SerializeField] private GameObject ownShip;
    [SerializeField] private GameObject healthBar;
    private GameObject shield;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        shield = GameObject.FindGameObjectWithTag("shield");
        spriteRenderer.forceRenderingOff = shield == null;
        healthBar.GetComponent<SpriteRenderer>().forceRenderingOff = shield == null;
        var shipPosition = ownShip.transform.position;
        transform.position = new Vector3(shipPosition.x, shipPosition.y - 28, 0);
        if (shield != null)
        {
            var health = shield.GetComponent<ShieldBubble>().shieldHealth;
            healthBar.transform.localScale = new Vector3((float)(health / ShieldGenerator.ShieldHealth), 1, 1);
        }
    }
}
