using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerShipBehaviour : MonoBehaviour
    {        
        [SerializeField] private float movementSpeed = 0.1f;
        [SerializeField] private Transform exhaust;
        Rigidbody2D ownShip;
        public int turretsCount { get; set; }
        public double health { get; set; }
        private float timeDamaged;
        private SpriteRenderer spriteRenderer;
        private AudioSource audio;
        private GameObject shield;
        public readonly List<Vector2> positionsList = new()
            {
                new Vector2(3, 0), 
                new Vector2(-3, 0),
                new Vector2(2.5f, -5.5f),
                new Vector2(-2.5f, -5.5f),
                new Vector2(0, 4)
            };
        
        void Start()
        {
            health = 300;
            ownShip = GetComponent<Rigidbody2D>();
            audio = GetComponent<AudioSource>();
            audio.volume = PlayerPrefs.GetFloat("soundVolume");
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        void Update()
        {
            if (timeDamaged <= 0) 
                spriteRenderer.color = Color.white;
            else
                timeDamaged -= Time.deltaTime;
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");
            var movement = new Vector2(horizontalInput, verticalInput);
            movement = Vector2.ClampMagnitude(movement, movementSpeed);
            if(movement != Vector2.zero && Time.timeScale != 0)
            {
                ownShip.MovePosition(ownShip.position + movement);
                var direction = new Vector3(horizontalInput, verticalInput, 0);
                if (direction.magnitude > 0.1f) // проверяем, что есть движение
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                exhaust.localScale = new Vector3(22, 10, 1);
            }
            else
            {
                exhaust.localScale = new Vector3(1, 1, 1);
                audio.Play();
            }
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            var damage = collision.gameObject.GetComponent<IEnemy>().damage;
            shield = GameObject.FindGameObjectWithTag("shield");
            if (shield != null &&
                shield.GetComponent<ShieldBubble>().state == ShieldBubble.ShieldStates.Online)
            {
                var shieldHealth = shield.GetComponent<ShieldBubble>().shieldHealth;
                shield.GetComponent<ShieldBubble>().shieldHealth = shieldHealth - damage / 60.0;
            }
            else
            {
                if (damage > 0)
                {
                    spriteRenderer.color = Color.red;
                    health -= damage / 60.0;
                    timeDamaged = 0.2f;
                }
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            spriteRenderer.color = Color.white;
        }

        public void TakeDamage(double damage)
        {
            shield = GameObject.FindGameObjectWithTag("shield");
            if (shield != null &&
                shield.GetComponent<ShieldBubble>().state == ShieldBubble.ShieldStates.Online)
            {
                var shieldHealth = shield.GetComponent<ShieldBubble>().shieldHealth;
                shield.GetComponent<ShieldBubble>().shieldHealth = shieldHealth - damage / 60.0;
            }
            else
            {
                health -= damage;
                timeDamaged = 0.2f;
            }
        }
    }
}
