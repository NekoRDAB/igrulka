using UnityEngine;

public class MiniNuke : MonoBehaviour
{    
    [SerializeField] private GameObject NukeExplosion;
    public Rigidbody2D rb;
    private AudioSource audioSource;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * MiniNukeTurret.Speed;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.volume = PlayerPrefs.GetFloat("soundVolume");
    }
    
    void Update()
    {
        var distance = Vector2.Distance(transform.position, MiniNukeTurret.ownShip.transform.position);
        if (distance > 150)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        var enemy = hitInfo.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(MiniNukeTurret.Damage);
            Instantiate(NukeExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
