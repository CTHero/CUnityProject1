using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float bounceStrength = 10.0F;
    [SerializeField] private AudioSource bounceSoundEffect;

    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Inside Bounce: OnTriggerEnter");
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Found Player");
            rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, bounceStrength);

            //Play the bounce sound effect if it exists
            if(bounceSoundEffect != null)   bounceSoundEffect.Play();

        }

    }

}
