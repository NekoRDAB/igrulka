using UnityEngine;

public class ProtonTorpedo : MonoBehaviour
{    
    [SerializeField] private GameObject hitEffect;
    public Rigidbody2D rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * ProtonTorpedoesTurret.Speed;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.volume = PlayerPrefs.GetFloat("soundVolume");
    }
    
    void Update()
    {
        var distance = Vector2.Distance(transform.position, ProtonTorpedoesTurret.ownShip.transform.position);
        if (distance > 150)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        var enemy = hitInfo.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(ProtonTorpedoesTurret.Damage);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
