using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFoot : MonoBehaviour
{
    private int footContact_ = 0;
    [SerializeField] private Rigidbody2D rb;

    public int FootContact_ => footContact_;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            footContact_++;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            footContact_--;
        }
    }
    
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }
}
