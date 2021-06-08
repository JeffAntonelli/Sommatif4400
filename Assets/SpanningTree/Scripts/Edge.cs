namespace SpanningTree.Scripts {
    public struct Edge {
        
        public int Source;
        public int Destination;
        public float Weight;

        public override string ToString() {
            return "Source : " + Source + " - Destination : " + Destination + " - Weight : " + Weight;
        }
        
    }
}