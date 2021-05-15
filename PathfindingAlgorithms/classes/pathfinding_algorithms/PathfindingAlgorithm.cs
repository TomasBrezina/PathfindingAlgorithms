﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PathfindingAlgorithms
{

    public abstract class PathfindingAlgorithm
    {
        protected Node StartNode;
        protected Node EndNode;
        protected List<Node> Graph;

        public Path Path;
        public Exist PathExists = Exist.Unknown;
        public bool PathFound = false;

        public PathfindingAlgorithm(Node startNode, Node endNode, List<Node> graph)
        {
            StartNode = startNode;
            EndNode = endNode;
            Graph = graph;
            Path = new Path();
        }
        public abstract void Step();
    }
}