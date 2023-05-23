using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShipAnimation : MonoBehaviour
{
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject stop;
    [SerializeField] private GameObject flare;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool isStopped;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if(!PlayerPrefs.HasKey("soundVolume"))
            PlayerPrefs.SetFloat("soundVolume", 1);
        audioSource.volume = PlayerPrefs.GetFloat("soundVolume") * 0.7f;
        isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        var topPos = top.transform.position.y;
        if (topPos >= stop.transform.position.y && !isStopped)
        {
            audioSource.Play();
            rb.velocity = Vector2.zero;
            var prevScale = flare.transform.localScale;
            flare.transform.localScale = new Vector3(prevScale.x * 3, prevScale.y, prevScale.z);
            isStopped = true;
        }
        else if (!isStopped)
            rb.MovePosition(rb.position + (Vector2)transform.up * transform.localScale * 80 * Time.deltaTime);
    }
}
