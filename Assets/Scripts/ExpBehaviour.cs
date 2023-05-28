using UnityEngine;

public class ExpBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject ownShip;    
    [SerializeField] public float pickUpRange;
    public bool IsMagnetized;
    private GameStateController gameStateController;
    private float expSpeed = 1.5f;
    private Rigidbody2D exp;
    
    void Start()
    {
        exp = GetComponent<Rigidbody2D>();
        ownShip = GameObject.Find("OwnShip");
        gameStateController = GameObject.Find("GameStateController").GetComponent<GameStateController>();
    }
    
    void Update()
    {
        var distance = Vector2.Distance(ownShip.transform.position, exp.transform.position);
        if (IsMagnetized)
        {
            var movement = (Vector2)(ownShip.transform.position - exp.transform.position);
            movement = Vector2.ClampMagnitude(movement, 5);
            exp.MovePosition(exp.position + movement);
        }

        else if (pickUpRange > distance)
        {
            var movement = (Vector2)(ownShip.transform.position - exp.transform.position);
            movement = Vector2.ClampMagnitude(movement, expSpeed);
            exp.MovePosition(exp.position + movement);
        }

        if (distance < 5)
        {
            gameStateController.AddExp();
            Destroy(gameObject);
        }
    }
}
