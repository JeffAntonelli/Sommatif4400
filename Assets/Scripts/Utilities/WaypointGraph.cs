using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using path; 
using System;
using System.Runtime.CompilerServices;
using UnityEditor;

public class WaypointGraph : MonoBehaviour
{
    private Graph graph1_ = new Graph();

    [SerializeField] private float CamResolution = 0.5f;

    private List<Vector3> GenerateShortestPath(Vector3 startPos, Vector3 EndPos)
    {
        graph1_.Nodes[0].position// Get closest start nodeIndex, get closest end nodeIndex.
    }
    // Start is called before the first frame update
    void Start()
    {
        var mainCamera = Camera.main;
        var cameraSize = 2.0f * mainCamera.orthographicSize * new Vector2(mainCamera.aspect, 1.0f);
        var cameraRect = new Rect() {min = -cameraSize / 2.0f, max = cameraSize / 2.0f};

        var width = cameraRect.width / CamResolution;
        var height = cameraRect.height / CamResolution;
        Dictionary<Vector2Int, int> nodeMap = new Dictionary<Vector2Int, int>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var worldPos = new Vector2(x, y) * CamResolution - cameraRect.size / 2.0f;
                var raycast = Physics2D.Raycast(worldPos, Vector2.up, CamResolution / 2.0f);
                if (raycast.collider != null)
                    continue;
                raycast = Physics2D.Raycast(worldPos, Vector2.down, CamResolution / 2.0f);
                if (raycast.collider != null)
                    continue;
                raycast = Physics2D.Raycast(worldPos, Vector2.left, CamResolution / 2.0f);
                if (raycast.collider != null)
                    continue;
                raycast = Physics2D.Raycast(worldPos, Vector2.right, CamResolution / 2.0f);
                if (raycast.collider != null)
                    continue;
                nodeMap[new Vector2Int(x, y)] = graph1_.AddNode(worldPos);
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
                        graph1_.AddNeighborEdge(nodeIndex, nodeMap[neighborPos]);
                    }
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        for (int i = 0; i < graph1_.Nodes.Count; i++)
        {
            var node = graph1_.Nodes[i];

            foreach (var neighbor in node.neighbors)
            {
                Gizmos.color = path.Contains(i) && path.Contains(neighbor.nodeIndex) ? Color.red : Color.blue;
                Gizmos.DrawLine(node.position, graph1_.Nodes[neighbor.nodeIndex].position);
            }
        }

    }
}
