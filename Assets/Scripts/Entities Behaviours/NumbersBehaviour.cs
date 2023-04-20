using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class NumbersBehaviour : MonoBehaviour
{
    private Rigidbody2D numbersBody;
    private Stopwatch timeAlive;

    [SerializeField] private TextMeshProUGUI damageNumber;
    // Start is called before the first frame update
    void Start()
    {
        numbersBody = GetComponent<Rigidbody2D>();
        numbersBody.velocity = new Vector2(Random.Range(-10, 10), 40);
        timeAlive = new Stopwatch();
        timeAlive.Start();
        damageNumber = GameObject.Find("DamageNumber").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAlive.ElapsedMilliseconds > 2000)
        {
            timeAlive.Stop();
            Destroy(gameObject);
        }
    }

    public void SetNumber(int number)
    {
        damageNumber.text = number.ToString();
        Start();
    }
}
