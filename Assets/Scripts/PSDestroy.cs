using UnityEngine;

namespace Assets._2D_Space_Kit.Scripts
{
    public class PSDestroy : MonoBehaviour 
    {
        void Start () 
        {
            Destroy(gameObject, GetComponent<ParticleSystem>().duration);
        }
    }
}
