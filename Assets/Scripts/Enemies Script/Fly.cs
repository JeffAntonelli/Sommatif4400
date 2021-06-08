using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Fly : MonoBehaviour
{
    public WaypointGraph waypointGraph;
    [SerializeField] private float speed;
    [SerializeField] private GameObject flyGameObject;
    public GameObject playerGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 startPost = Vector2.zero; 
        waypointGraph.ShortestPath(transform.position, playerGameObject.transform.position);
    }
}
