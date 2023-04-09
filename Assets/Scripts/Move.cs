using UnityEngine;

namespace Assets.Scripts
{
    public class Move : MonoBehaviour
    {
        Rigidbody2D ownShip;
        float movementSpeed = 0.1f;
        // Start is called before the first frame update
        void Start()
        {
            ownShip = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            var movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movement = Vector2.ClampMagnitude(movement, movementSpeed);
            if(movement != Vector2.zero)
            {
                ownShip.MovePosition(ownShip.position + movement);
                transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(movement.x, movement.y)*180/Mathf.PI);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            print("collision");
        }

        public void OnTrigger2D(Collider2D collision)
        {
            ownShip.position = new Vector2(-6, 4);
        }
    }
}
