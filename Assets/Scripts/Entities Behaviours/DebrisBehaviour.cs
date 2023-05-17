using UnityEngine;
using Random = System.Random;

public class DebrisBehaviour : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject damageNumbers;
    [SerializeField] private GameObject repairKit;
    [SerializeField] private GameObject freeze;
    [SerializeField] private GameObject magnet;
    [SerializeField] private GameObject destruction;
    private Random random;
    public int damage { get; set; }
    private double health = 10;
    
    void Start()
    {
        random = new Random();
        damage = 0;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        var damageNumber = Instantiate(damageNumbers, transform.position, Quaternion.identity);
        damageNumber.GetComponent<NumbersBehaviour>().SetNumber(damage);
        if (health <= 0)
            Die();
    }

    public void Freeze(float time)
    {
        
    }

    void Die()
    {
        var randomNumber = random.NextDouble();
        
        switch (randomNumber)
        {
            case < 0.17:
                Instantiate(freeze, transform.position, Quaternion.identity);
                break;
            case < 0.34:
                Instantiate(magnet, transform.position, Quaternion.identity);
                break;
            case < 0.51:
                Instantiate(destruction, transform.position, Quaternion.identity);
                break;
            default:
                Instantiate(repairKit, transform.position, Quaternion.identity);
                break;
        }
        
        Destroy(gameObject);
    }
}
