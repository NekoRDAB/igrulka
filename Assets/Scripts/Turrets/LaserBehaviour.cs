using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        var enemy = hitInfo.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(LaserTurretBehaviour.Damage);
        }
    }
}
