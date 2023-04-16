using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bulletPrefab;

    private float elapsed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();

        elapsed += Time.deltaTime;
        if (elapsed >= 0.5f)
        {
            Shoot();
            elapsed = 0f;
        }
    }

    void Shoot()
    {
        // Shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}