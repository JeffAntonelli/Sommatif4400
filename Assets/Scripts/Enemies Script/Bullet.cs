using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    //Handle the bullets
    
    [SerializeField] private GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.2f);
        Destroy(gameObject);
       // FindObjectOfType<AudioManager>().Play("Boom");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.2f);
        Destroy(gameObject);
        //FindObjectOfType<AudioManager>().Play("Boom");
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
