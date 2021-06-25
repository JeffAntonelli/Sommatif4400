using System;
using UnityEngine;

namespace SpanningTree.Scripts {
    [Serializable]
    public struct WaypointConnection {
        
        public Transform From;
        public Transform To;
        

        public WaypointConnection(Transform from, Transform to) {
            From = from;
            To = to;
        }
        
    }
}