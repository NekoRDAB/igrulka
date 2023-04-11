using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class Move : MonoBehaviour
    {
        Rigidbody2D ownShip;
        [SerializeField] private float movementSpeed = 0.1f;

        [SerializeField] private Transform exhaust;

        public double health;
        [SerializeField] private GameOverScreen gameOverScreen;

        // Start is called before the first frame update
        void Start()
        {
            health = 100;
            ownShip = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            var movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (movement == Vector2.zero)
                exhaust.localScale = new Vector3(1, 1, 1);
            else
                exhaust.localScale = new Vector3(10, 3, 1);
            movement = Vector2.ClampMagnitude(movement, movementSpeed);
            if(movement != Vector2.zero)
            {
                ownShip.MovePosition(ownShip.position + movement);
                transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(movement.x, movement.y)*180/Mathf.PI);
            }

            if (health <= 0)
            {
                gameOverScreen.SetUp();
            }
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            var damage = collision.gameObject.GetComponent<EnemyMovement>().enemy.Damage;
            health -= damage / 60.0;
        }
    }
}
