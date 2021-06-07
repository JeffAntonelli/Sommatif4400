using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRight : MonoBehaviour
{
    // Handle the enemy patrol

    [SerializeField] float speed;
    [SerializeField] float distance;
    
    private bool movingRight_ = true;
    
    [SerializeField] Transform groundDetection;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
            if (movingRight_ == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight_ = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0,-180,0);
                movingRight_ = true;
            }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Foot"))
        {
            Destroy(gameObject);
        }
    }
}
