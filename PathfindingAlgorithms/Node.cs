﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PathfindingAlgorithms
{
    public class Edge
    {
        public Node Node;
        public float Weight;
        public Edge(Node toNode, float weight)
        {
            Node = toNode;
            Weight = weight;
        }
    }
    public class Node
    {
        public int ID;
        public double[] Pos;
        public List<Edge> Edges;
        public NodeType Type;
        public NodeState State;
        public Shape Shape;

        public Node(int id, double[] pos, Shape shape)
        {
            ID = id;
            Pos = pos;
            Edges = new List<Edge>();
            Type = NodeType.Empty;
            State = NodeState.Unseen;

            // default shape
            Shape = shape;
            Shape.Tag = id;
            Shape.Fill = NodeBrushes.Empty;
            Shape.Stroke = Brushes.Gray;
            Shape.StrokeThickness = 0.5;
        }
        public void MarkAs(Brush b) { Shape.Fill = b; }
        public void SetDefaultState() { SetState(NodeState.Unseen); }
        public void SetDefaultType() { SetType(NodeType.Empty); }

        // Set state of the node
        public void SetState(NodeState state) {
            State = state;
            if (Type == NodeType.Empty)
            {
                MarkAs(NodeBrushes.FromState[state]);
            }
        }
        // Set type of the node
        public void SetType(NodeType type)
        {
            Type = type;
            MarkAs(NodeBrushes.FromType[type]);
        }

        // Add neighbour and weight to Neigbours list 
        public void AddEdge(Node node, float weight)
        {
            Edges.Add(new Edge(node, weight));
        }
    }
}
