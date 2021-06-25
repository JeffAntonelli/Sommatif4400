using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpanningTree.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class Fly : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Foot") || other.gameObject.CompareTag("Spikes"))
        {
            Destroy(gameObject);
        }
    }
}
