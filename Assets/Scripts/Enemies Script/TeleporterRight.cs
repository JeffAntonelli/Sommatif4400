using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class TeleporterRight : MonoBehaviour
{
    //Player can interact with a door to "teleport" to another door
    
    [SerializeField] GameObject rightPipeEntry;
    [SerializeField] GameObject rightPipeExit;
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
        enemy.transform.position = new Vector3(rightPipeExit.transform.position.x, rightPipeExit.transform.position.y, 
            0.0f);
    }
}