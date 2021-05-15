using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    public static class Tools
    {
        public static List<string> AlgorithmsList = new List<string>()
        {
            "Dijkstra",
            "BFS",
            "A Star"
        };

        public static string AlgorithmToLink(string name)
        {
            switch (name)
            {
                case "BFS": return "https://en.wikipedia.org/wiki/Breadth-first_search";
                case "Dijkstra": return "https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm";
                case "A Star": return "https://en.wikipedia.org/wiki/A*_search_algorithm";
                default: return null;
            }
        }
        public static PathfindingAlgorithm AlgorithmFromString(string name, Enviroment env)
        {
            switch (name)
            {
                case "BFS":
                    return new BFS(env.StartNode, env.EndNode, env.Nodes);
                case "Dijkstra":
                    return new Dijkstra(env.StartNode, env.EndNode, env.Nodes);
                case "A Star":
                    return new AStar(env.StartNode, env.EndNode, env.Nodes, env.HeuristicDist);
                default:
                    return null;
            }
        }

        public static WallGenerator WallGeneratorFromString(string name, Enviroment env)
        {
            switch (name)
            {
                case "Noise generator":
                    return new NoiseWallGenerator(env, 30);
                case "Recursive subdivision":
                    // if env is inherited
                    if (env.GetType().IsSubclassOf(typeof(GridEnviroment)))
                    {
                        return new RecursiveSubdivision((GridEnviroment) env);
                    }
                    else return null;
                case "Clear walls":
                    return new ClearWalls(env);
                case "Prim algorithm":
                    return new PrimWallGenerator(env);
                default:
                    return null;
            }
        }
    }
}
