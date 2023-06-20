using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioSource collectionSoundEffect;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Mushroom")
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            rb.velocity = new Vector3(rb.velocity.x, 14f);
        }
    }
}
