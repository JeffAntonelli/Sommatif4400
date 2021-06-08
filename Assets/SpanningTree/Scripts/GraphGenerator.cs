using System.Collections.Generic;
using UnityEngine;

namespace SpanningTree.Scripts {
    [ExecuteAlways]
    public class GraphGenerator : MonoBehaviour {
        
        private readonly List<Transform> _waypoints = new List<Transform>();
        private List<WaypointConnection> _connections;
        private Edge[] _edges;
        
        private void Update() {
            _waypoints.Clear();
            foreach (Transform child in transform) {
                _waypoints.Add(child);
            }
            _connections = new List<WaypointConnection>();
            foreach (Transform from in _waypoints) {
                foreach (Transform to in _waypoints) {
                    if (to == from) continue;
                    if (Physics2D.Linecast(to.position, from.position)) continue;
                    _connections.Add(new WaypointConnection(from, to));
                }
            }
            // Generate Kruskal tree
            Graph graph = KruskalUtility.CreateGraph(_waypoints.Count, _connections.Count);
            for (int index = 0; index < _connections.Count; index++) {
                WaypointConnection waypointConnection = _connections[index];
                int fromIndex = _waypoints.IndexOf(waypointConnection.From);
                int toIndex = _waypoints.IndexOf(waypointConnection.To);
                graph.Edges[index] = new Edge {
                    Source = fromIndex,
                    Destination = toIndex,
                    Weight = Vector2.Distance(_waypoints[fromIndex].transform.position, _waypoints[toIndex].transform.position)
                };
            }
            _edges = KruskalUtility.ShortestPath(graph);
        }
        
        public List<Vector2> ShortestPath(Vector2 startPos, Vector2 endPos)
        {
            int startPosIndex = NearestNodeIndex(startPos);

            int endPosIndex = NearestNodeIndex(endPos);

            //List<int> posIndex = _edges.DijkstraAlgorithm(startPosIndex, endPosIndex);

            List<Vector2> path = new List<Vector2>();
        
            //foreach (int index in posIndex)
            {
              //  path.Add(_edges.Nodes[index].position);
            }

            return path;
        }

        private int NearestNodeIndex(Vector2 vector2)
        {
            int shortestStartNode = 0;
            float shortestStartNodeDistance = float.MaxValue;

            /*for (var index = 0; index < graph.Nodes.Count; index++)
            {
                Node node = graph.Nodes[index];
                if (shortestStartNodeDistance < Vector2.Distance(node.position, vector2))
                {
                    shortestStartNodeDistance = Vector2.Distance(node.position, vector2);
                    shortestStartNode = index;
                }
            }*/

            return shortestStartNode;
        }

        private void OnDrawGizmos() {
            // Display Network
            foreach (WaypointConnection waypointConnections in _connections) {
                if (waypointConnections.From == null || waypointConnections.To == null) continue;
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(waypointConnections.From.transform.position, waypointConnections.To.transform.position);
            }
            // Display Kruskal tree
            if (_edges == null) return;
            foreach (Edge edge in _edges) {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(_waypoints[edge.Source].transform.position, _waypoints[edge.Destination].transform.position);
            }
        }

    }
}