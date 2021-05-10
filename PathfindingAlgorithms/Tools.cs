﻿using System;
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

        public static WallGenerator WallGeneratorFromString(string name, Enviroment env)
        {
            switch (name)
            {
                case "Noise generator":
                    return new NoiseWallGenerator(env, 30);
                case "Recursive subdivision":
                    return new RecursiveSubdivision(env);
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
