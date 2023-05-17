using UnityEngine;

public class FlakProjectileLogic : MonoBehaviour
{    
    [SerializeField] private GameObject hitEffect;
    public Rigidbody2D rb;
    private AudioSource audioSource;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (transform.up + transform.right*Random.Range(-0.2f, 0.2f)) * FlakTurret.Speed;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.volume = PlayerPrefs.GetFloat("soundVolume");
    }
    
    void Update()
    {
        var distance = Vector2.Distance(transform.position, FlakTurret.ownShip.transform.position);
        if (distance > 150)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        var enemy = hitInfo.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(FlakTurret.Damage);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
