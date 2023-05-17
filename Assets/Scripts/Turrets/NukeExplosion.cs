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
            transform.localScale *= 1.035f;
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        var enemy = hitInfo.GetComponent<IEnemy>();
        if (enemy != null)
            enemy.TakeDamage(MiniNukeTurret.Damage);
    }
}
