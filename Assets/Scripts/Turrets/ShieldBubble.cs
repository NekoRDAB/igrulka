using System.Diagnostics;
using UnityEngine;

public class ShieldBubble : MonoBehaviour
{
    public double shieldHealth;
    public ShieldStates state;
    private Stopwatch timer;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D collider;
    public enum ShieldStates
    {
        Offline,
        Recharging,
        Online
    }

    void Start()
    {
        state = ShieldStates.Online;
        shieldHealth = ShieldGenerator.ShieldHealth;
        timer = new Stopwatch();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
    }
    
    void Update()
    {
        if (shieldHealth <= 0 && state == ShieldStates.Online)
        {
            spriteRenderer.forceRenderingOff = true;
            collider.enabled = false;
            state = ShieldStates.Offline;
        }

        if (state == ShieldStates.Offline)
        {
            state = ShieldStates.Recharging;
            timer.Start();
        }

        if (state == ShieldStates.Recharging
            && timer.Elapsed.Seconds > ShieldGenerator.CoolDown)
        {
            state = ShieldStates.Online;
            timer.Stop();
            timer.Reset();
            spriteRenderer.forceRenderingOff = false;
            collider.enabled = true;
            shieldHealth = ShieldGenerator.ShieldHealth;
        }
    }
}
