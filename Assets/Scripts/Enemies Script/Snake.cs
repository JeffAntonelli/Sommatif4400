using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Handle the enemy patrol

    [SerializeField] float speed;

    private bool movingLeft_ = true;
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Foot"))
        {
            Destroy(gameObject);
        }
    }
}
