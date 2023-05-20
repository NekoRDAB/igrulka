using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShipAnimation : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject top;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * 50;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("soundVolume");
    }

    // Update is called once per frame
    void Update()
    {
        var topPos = top.transform.position.y;
        if (topPos <= camera.rect.yMin)
            rb.velocity = Vector2.zero;
    }
}
