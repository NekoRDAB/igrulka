using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject ownShip;    
    [SerializeField] public float pickUpRange;
    public bool IsMagnetized;
    private GameStateController gameStateController;
    private float chestSpeed = 1;
    private Rigidbody2D chest;
    
    void Start()
    {
        chest = GetComponent<Rigidbody2D>();
        ownShip = GameObject.Find("OwnShip");
        gameStateController = GameObject.Find("GameStateController").GetComponent<GameStateController>();
    }
    
    void Update()
    {
        var distance = Vector2.Distance(ownShip.transform.position, chest.transform.position);
        if (IsMagnetized)
        {
            var movement = (Vector2)(ownShip.transform.position - chest.transform.position);
            movement = Vector2.ClampMagnitude(movement, 5);
            chest.MovePosition(chest.position + movement);
        }

        else if (pickUpRange > distance)
        {
            var movement = (Vector2)(ownShip.transform.position - chest.transform.position);
            movement = Vector2.ClampMagnitude(movement, chestSpeed);
            chest.MovePosition(chest.position + movement);
        }

        if (distance < 5)
        {
            if (gameStateController.level <= 25)
                gameStateController.LevelUp();
            gameStateController.level++;
            Destroy(gameObject);
        }
    }
}