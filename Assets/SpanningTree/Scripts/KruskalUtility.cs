using System;

namespace SpanningTree.Scripts {
	public static class KruskalUtility {
		
		public static Graph CreateGraph(int verticesCount, int edgesCoun) {
			Graph graph = new Graph {VerticesCount = verticesCount, EdgesCount = edgesCoun};
			graph.Edges = new Edge[graph.EdgesCount];
        
            return graph;
        }
        
        private static int Find(Subset[] subsets, int i) { 
            if (subsets[i].Parent != i) subsets[i].Parent = Find(subsets, subsets[i].Parent);
        	return subsets[i].Parent;
        }
        
        private static void Union(Subset[] subsets, int x, int y) {
            int xroot = Find(subsets, x);
            int yroot = Find(subsets, y);
            if (subsets[xroot].Rank < subsets[yroot].Rank) subsets[xroot].Parent = yroot;
            else if (subsets[xroot].Rank > subsets[yroot].Rank) subsets[yroot].Parent = xroot;
            else {
                subsets[yroot].Parent = xroot;
                ++subsets[xroot].Rank;
            }
        }

        public static Edge[] ShortestPath(Graph graph) {
            int verticesCount = graph.VerticesCount;
            Edge[] result = new Edge[verticesCount];
            int i = 0;
            int e = 0;
            Array.Sort(graph.Edges, (a, b) => a.Weight.CompareTo(b.Weight));
            Subset[] subsets = new Subset[verticesCount];
            for (int v = 0; v < verticesCount; ++v) {
                subsets[v].Parent = v;
                subsets[v].Rank = 0;
            }
            while (e < verticesCount - 1) {
                if (graph.Edges.Length <= ++i) break;
                Edge nextEdge = graph.Edges[i];
                int x = Find(subsets, nextEdge.Source);
                int y = Find(subsets, nextEdge.Destination);
                if (x != y) {
                	result[e++] = nextEdge;
                	Union(subsets, x, y);
                }
            }
            return result;
        }
        
        
        
	}
}