using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform ownShip;
        [SerializeField] private Vector3 target;
        [SerializeField] private float speed = 1.5f;

        void FixedUpdate ()
        {
            var position = ownShip.transform.position;
            target = new Vector3(position.x, position.y, -10);
            transform.position = Vector3.Lerp(transform.position, target, speed * Time.fixedDeltaTime);
        }
    }
}