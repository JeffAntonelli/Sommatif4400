using System.Collections;

namespace path
{
    public struct Neighbor
    {
        public int nodeIndex;
        public float length;

        public Neighbor(int nodeIndex, float length)
        {
            this.nodeIndex = nodeIndex;
            this.length = length;
        }
    }
}
