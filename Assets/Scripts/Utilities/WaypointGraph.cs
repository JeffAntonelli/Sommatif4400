using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using path; 
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine.Serialization;

public class WaypointGraph : MonoBehaviour
{
    public Graph graph = new Graph();

    [SerializeField] private float camResolution = 0.5f;
    
    private List<int> path = new List<int>();

    public List<Vector2> ShortestPath(Vector2 startPos, Vector2 endPos)
    {
        int startPosIndex = NearestNodeIndex(startPos);

        int endPosIndex = NearestNodeIndex(endPos);

        List<int> posIndex = graph.DijkstraAlgorithm(startPosIndex, endPosIndex);

        List<Vector2> path = new List<Vector2>();
        
        foreach (int index in posIndex)
        {
            path.Add(graph.Nodes[index].position);
        }

        return path;
    }

    private int NearestNodeIndex(Vector2 vector2)
    {
        int shortestStartNode = 0;
        float shortestStartNodeDistance = float.MaxValue;

        for (var index = 0; index < graph.Nodes.Count; index++)
        {
            Node node = graph.Nodes[index];
            if (shortestStartNodeDistance < Vector2.Distance(node.position, vector2))
            {
                shortestStartNodeDistance = Vector2.Distance(node.position, vector2);
                shortestStartNode = index;
            }
        }

        return shortestStartNode;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var mainCamera = Camera.main;
        var cameraSize = 2.0f * mainCamera.orthographicSize * new Vector2(mainCamera.aspect, 1.0f);
        var cameraRect = new Rect() {min = -cameraSize / 2.0f, max = cameraSize / 2.0f};

        var width = cameraRect.width / camResolution;
        var height = cameraRect.height / camResolution;
        Dictionary<Vector2Int, int> nodeMap = new Dictionary<Vector2Int, int>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var worldPos = new Vector2(x, y) * camResolution - cameraRect.size / 2.0f;
                var raycast = Physics2D.Raycast(worldPos, Vector2.up, camResolution / 2.0f)
                    ;
                if (raycast.collider != null)
                    continue;
                raycast = Physics2D.Raycast(worldPos, Vector2.down, camResolution / 2.0f);
                if (raycast.collider != null)
                    continue;
                raycast = Physics2D.Raycast(worldPos, Vector2.left, camResolution / 2.0f);
                if (raycast.collider != null)
                    continue;
                raycast = Physics2D.Raycast(worldPos, Vector2.right, camResolution / 2.0f);
                if (raycast.collider != null)
                    continue;
                nodeMap[new Vector2Int(x, y)] = graph.AddNode(worldPos);
            }
        }

        foreach (var nodePair in nodeMap)
        {
            var pos = nodePair.Key;
            var nodeIndex = nodePair.Value;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    var neighborPos = pos + new Vector2Int(dx, dy);
                    if (neighborPos != pos && nodeMap.ContainsKey(neighborPos))
                    {
                        graph.AddNeighborEdge(nodeIndex, nodeMap[neighborPos]);
                    }
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        for (int i = 0; i < graph.Nodes.Count; i++)
        {
            var node = graph.Nodes[i];

            foreach (var neighbor in node.neighbors)
            {
                Gizmos.color = path.Contains(i) && path.Contains(neighbor.nodeIndex) ? Color.red : Color.blue;
                Gizmos.DrawLine(node.position, graph.Nodes[neighbor.nodeIndex].position);
            }
        }
    }
}
