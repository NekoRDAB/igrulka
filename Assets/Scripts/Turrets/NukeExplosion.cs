using UnityEngine;

public class NukeExplosion : MonoBehaviour
{
    private AudioSource audio;
    private float timeAlive;
    
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = PlayerPrefs.GetFloat("soundVolume");
    }
    
    void Update()
    {
        if (timeAlive > MiniNukeTurret.Duration)
        {
            timeAlive = 0;
            Destroy(gameObject);
        }
        else
        {
            timeAlive += Time.deltaTime;
            transform.localScale *= 1 + 2.5f * Time.deltaTime;
            if (transform.localScale.magnitude >= 5 * MiniNukeTurret.ownShip.transform.localScale.magnitude)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        var enemy = hitInfo.GetComponent<IEnemy>();
        if (enemy != null)
            enemy.TakeDamage(MiniNukeTurret.Damage);
    }
}
