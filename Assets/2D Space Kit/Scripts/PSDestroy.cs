using UnityEngine;

namespace Assets._2D_Space_Kit.Scripts
{
    public class PSDestroy : MonoBehaviour {

        // Use this for initialization
        void Start () {
            Destroy(gameObject, GetComponent<ParticleSystem>().duration);
        }
	
        // Update is called once per frame
        void Update () {
	
        }
    }
}
