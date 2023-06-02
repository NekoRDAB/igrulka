using Assets.Scripts;
using System.Diagnostics;
using UnityEngine;

public class ExplosiveEnemyBehaviour : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject exp;
    [SerializeField] private GameObject damageNumbers;    
    [SerializeField] private float speed;
    [SerializeField] public double health;
    public int damage { get; set; }


    private Rigidbody2D ship;
    private SpriteRenderer spriteRenderer;
    private Stopwatch timeDamaged;
    private AudioSource audio;
    private Color defaultColor;
    private float timeFrozen;

    void Start()
    {
        ship = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeDamaged = new Stopwatch();
        player = GameObject.Find("OwnShip");
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("soundVolume");
        damage = 30;
        health = 500;
        defaultColor = spriteRenderer.color;
    }
    
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 250)
            transform.position = GetTeleportPosition();

        if (timeFrozen <= 0)
        {
            if (spriteRenderer.color == Color.blue)
                spriteRenderer.color = defaultColor;
            var movement = (Vector2)player.transform.position - ship.position;
            movement = Vector2.ClampMagnitude(movement, speed);
            ship.MovePosition(ship.position + movement);
            transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(movement.x, movement.y) * 180 / Mathf.PI);
        }
        else
        {
            spriteRenderer.color = Color.blue;
            timeFrozen -= Time.deltaTime;
        }
        if (spriteRenderer.color == Color.red && timeDamaged.ElapsedMilliseconds > 400)
        {
            spriteRenderer.color = defaultColor;
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
        Instantiate(explosion, ship.position, Quaternion.identity);
        Instantiate(exp, ship.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Freeze(float time)
    {
        timeFrozen = time;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerShipBehaviour>();
        if (player != null)
            Explode(player);
    }

    private void Explode(PlayerShipBehaviour player)
    {
        player.TakeDamage(damage);
        Die();
    }

    private Vector3 GetTeleportPosition()
    {
        var offset = Quaternion.AngleAxis(UnityEngine.Random.Range(-180f, 180f), Vector3.back) * (new Vector3(150, 120, 0));
        return player.transform.position + offset;
    }
}
