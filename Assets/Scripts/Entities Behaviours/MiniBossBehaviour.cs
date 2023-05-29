using System.Diagnostics;
using UnityEngine;

public class MiniBossBehaviour : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;    
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject damageNumbers;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject healthBar;
    private float timeToShoot;
    private float shootingCoolDown;
    public int damage { get; set; }
    public int health { get; set; }
    private Rigidbody2D ship;
    private SpriteRenderer spriteRenderer;
    private Stopwatch timeDamaged;
    private AudioSource audio;
    private float timeFrozen;

    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeDamaged = new Stopwatch();
        player = GameObject.Find("OwnShip");
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("soundVolume");
        damage = 150;
        health = 1500;
        shootingCoolDown = 3f;
        timeToShoot = shootingCoolDown;
        transform.DetachChildren();
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 250)
            transform.position = GetTeleportPosition();

        if (timeToShoot <= 0)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            timeToShoot = shootingCoolDown;
        }
        else
            timeToShoot -= Time.deltaTime;
        if (timeFrozen <= 0)
        {
            var movement = (Vector2)player.transform.position - ship.position;
            movement = Vector2.ClampMagnitude(movement, speed);
            ship.MovePosition(ship.position + movement);
            transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(movement.x, movement.y) * 180 / Mathf.PI);
        }
        else
            timeFrozen -= Time.deltaTime;
        if (spriteRenderer.color == Color.red && timeDamaged.ElapsedMilliseconds > 400)
        {
            spriteRenderer.color = Color.white;
            timeDamaged.Stop();
            timeDamaged.Reset();
        }

    }

    public void TakeDamage(int damage)
    {
        audio.Play();
        health -= damage;
        if (timeFrozen <= 0)
        {
            var movement = ((Vector2)player.transform.position - ship.position).normalized;
            ship.AddForce(-movement * 8000);
        }
        var damageNumber = Instantiate(damageNumbers, ship.position, Quaternion.identity);
        damageNumber.GetComponent<NumbersBehaviour>().SetNumber(damage);
        spriteRenderer.color = Color.red;
        if (health <= 0)
        {
            Die();
            return;
        }
        timeDamaged.Start();
    }

    void Die()
    {
        Destroy(healthBar);
        Instantiate(chest, ship.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Freeze(float time)
    {
        timeFrozen = time;
    }

    private Vector3 GetTeleportPosition()
    {
        var offset = Quaternion.AngleAxis(UnityEngine.Random.Range(-180f, 180f), Vector3.back) * (new Vector3(200, 180, 0));
        return player.transform.position + offset;
    }
}
