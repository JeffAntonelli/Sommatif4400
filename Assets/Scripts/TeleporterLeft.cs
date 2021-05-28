using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class TeleporterLeft : MonoBehaviour
{
    //Player can interact with a door to "teleport" to another door
    
    [SerializeField] GameObject leftPipeEntry;
    [SerializeField] GameObject leftPipeExit;
    [SerializeField] private GameObject enemy;
   

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("pipe"))
        {
            StartCoroutine(Teleport());
        }
    }

    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds (0.1f);
        enemy.transform.position = new Vector3(leftPipeExit.transform.position.x, leftPipeExit.transform.position.y, 
            0.0f);
    }
}