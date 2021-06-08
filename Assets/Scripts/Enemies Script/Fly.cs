using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpanningTree.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class Fly : MonoBehaviour
{
    public GraphGenerator Graph;
    [SerializeField] private float speed;
    [SerializeField] GameObject playerGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("shortest path : " + String.Join(" ", Graph.ShortestPath(transform.position, playerGameObject.transform.position)));
        //Debug.Log("network : " + String.Join(" ", Graph.graph.Nodes.Select(node => node.position)));
        //GraphGenerator.ShortestPath(transform.position, playerGameObject.transform.position);
        //string debug = "";
        /*foreach (Vector2 vector2 in waypointGraph.ShortestPath(transform.position, playerGameObject.transform.position))
        {
            
            //transform.Translate(vector2.normalized * (speed * Time.deltaTime));
            debug += " " + vector2;
        
            Debug.Log(debug);
        }*/
    }
}
