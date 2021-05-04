using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    public static class Tools
    {
        public static Algorithm AlgorithmFromString(string name, Node startNode, List<Node> graph)
        {
            switch (name)
            {
                case "BFS":
                    return new BFS(startNode, graph);
                case "Dijkstra":
                    return new Dijkstra(startNode, graph);
                default:
                    return null;
            }
        }
    }
}
