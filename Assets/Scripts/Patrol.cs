using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    // Handle the enemy patrol

    [SerializeField] float speed;
    [SerializeField] float distance;
    
    private bool movingLeft_ = true;
    
    public Transform groundDetection;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
            if (movingLeft_ == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft_ = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0,-180,0);
                movingLeft_ = true;
            }
        {
            
        }
    }
}
