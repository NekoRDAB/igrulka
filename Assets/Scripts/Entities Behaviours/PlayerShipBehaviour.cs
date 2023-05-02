using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerShipBehaviour : MonoBehaviour
    {
        Rigidbody2D ownShip;
        public int turretsCount;
        [SerializeField] private float movementSpeed = 0.1f;
        [SerializeField] private Transform exhaust;
        public double health;
        private AudioSource audio;
        private GameObject shield;

        public readonly List<Vector2> positionsList = new List<Vector2>()
            {
                new Vector2(0.375f, -1.5f), 
                new Vector2(-0.375f, -1.5f),
                new Vector2(0.375f, -0.5f),
                new Vector2(-0.375f, -0.5f),
                new Vector2(0, 0.6f)
            };

        // Start is called before the first frame update
        void Start()
        {
            health = 100;
            ownShip = GetComponent<Rigidbody2D>();
            audio = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var movement = new Vector2(horizontalInput, verticalInput);
            if (movement.magnitude > 0.1f) // проверяем, что есть движение
            {
                ownShip.MovePosition(ownShip.position + movement * movementSpeed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(Vector3.forward, movement);
            }
            if (movement == Vector2.zero)
            {
                exhaust.localScale = new Vector3(1, 1, 1);
                audio.Play();
            }
            else
            {
                exhaust.localScale = new Vector3(10, 3, 1);
            }
            movement = Vector2.ClampMagnitude(movement, movementSpeed);
            if(movement != Vector2.zero)
            {
                ownShip.MovePosition(ownShip.position + movement);
                var direction = new Vector3(horizontalInput, verticalInput, 0);
                if (direction.magnitude > 0.1f) // проверяем, что есть движение
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                }
            }
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            var damage = collision.gameObject.GetComponent<EnemyBehaviour>().damage;
            shield = GameObject.FindGameObjectWithTag("shield");
            if (shield != null)
            {
                var shieldHealth = shield.GetComponent<ShieldBubble>().shieldHealth;
                shield.GetComponent<ShieldBubble>().shieldHealth = shieldHealth - damage / 60.0;
            }
            else
            {
                health -= damage / 60.0;
            }
        }
    }
}
