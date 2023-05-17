using System.Diagnostics;
using TMPro;
using UnityEngine;

public class NumbersBehaviour : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI damageNumber;
    private Rigidbody2D numbersBody;
    private Stopwatch timeAlive;

    void Start()
    {
        numbersBody = GetComponent<Rigidbody2D>();
        numbersBody.velocity = new Vector2(Random.Range(-10, 10), 40);
        timeAlive = new Stopwatch();
        timeAlive.Start();
        damageNumber = GameObject.Find("DamageNumber").GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        if (timeAlive.ElapsedMilliseconds > 2000)
        {
            timeAlive.Stop();
            Destroy(gameObject);
        }
    }

    public void SetNumber(int number, bool regen=false)
    {
        damageNumber.text = number.ToString();
        if (regen)
            damageNumber.color = Color.green;
        Start();
    }
}
