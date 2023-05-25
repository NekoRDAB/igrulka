using Assets.Scripts;
using UnityEngine;

public class HealthBar : MonoBehaviour
{    
    [SerializeField] private GameObject ownShip;
    private Transform healthBar;
    
    void Start()
    {
        healthBar = GetComponent<Transform>();
    }
    
    void Update()
    {
        var health = ownShip.GetComponent<PlayerShipBehaviour>().health;
        healthBar.localScale = new Vector3((float)(health / 100.0), 1, 1);
    }
}
