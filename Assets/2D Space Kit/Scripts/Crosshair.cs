using UnityEngine;

namespace Assets._2D_Space_Kit.Scripts
{
    public class Crosshair : MonoBehaviour {

        Vector3 wanted_position;
        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
            wanted_position = CustomPointer.pointerPosition;
            wanted_position.z = transform.position.z;
            transform.position =  wanted_position;
	
        }
    }
}
