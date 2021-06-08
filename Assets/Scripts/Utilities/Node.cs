using System.Collections.Generic;
using UnityEngine;

namespace path
{
    /** 
     * <summary>Simple node struct used for pathfinding</summary>
     */
    public struct Node
    {
        public Vector2 position;
        public List<Neighbor> neighbors;

        public Node(Vector2 position)
        {
            this.position = position;
            neighbors = new List<Neighbor>();
        }

    }
}