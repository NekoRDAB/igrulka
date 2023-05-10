using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // Start is called before the first frame update
    void Start()
    {
        random = new Random();
        damage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void TakeDamage(int damage)
    {
        health -= damage;
        
        var damageNumber = Instantiate(damageNumbers, transform.position, Quaternion.identity);
        damageNumber.GetComponent<NumbersBehaviour>().SetNumber(damage);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Freeze(float time)
    {
        
    }

    void Die()
    {
        var randomNumber = random.NextDouble();
        if (randomNumber < 0.17)
        {
            Instantiate(freeze, transform.position, Quaternion.identity);
        }
        else if (randomNumber < 0.34)
        {
            Instantiate(magnet, transform.position, Quaternion.identity);
        }
        else if (randomNumber < 0.51)
        {
            Instantiate(destruction, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(repairKit, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
