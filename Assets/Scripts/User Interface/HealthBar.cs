using Assets.Scripts;
using UnityEngine;

public class HealthBar : MonoBehaviour
{    
    [SerializeField] private GameObject ownShip;
    [SerializeField] private Transform healthBar;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        var ownShipPosition = ownShip.transform.position;
        transform.position = new Vector3(ownShipPosition.x, ownShipPosition.y - 26, 0);
        var health = ownShip.GetComponent<PlayerShipBehaviour>().health;
        healthBar.localScale = new Vector3((float)(health / 100.0), 1, 1);
    }
}
