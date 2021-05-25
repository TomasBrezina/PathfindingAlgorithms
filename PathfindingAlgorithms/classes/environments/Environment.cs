using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PathfindingAlgorithms
{
    public abstract class Environment
    {
        public Node StartNode;
        public Node EndNode;

        /// <summary> List of Nodes to store graph. </summary>
        public List<Node> Nodes;

        protected List<Path> Paths;
        protected Canvas Canv;

        /// <summary> Default width of canvas </summary>
        protected const double DefaultCanvWidth = 1000;

        public Environment(Canvas canv)
        {
            Paths = new List<Path>();
            Nodes = new List<Node>();
            Canv = canv;
        }

        /// <summary> Initialize Environment. </summary>
        public abstract void Initialize();

        /// <summary> Smallest possible distance from one node to another </summary>
        public abstract float HeuristicDist(Node start, Node end);


        /// <summary> Click event - assigned to <c>Node.Shape</c> </summary>
        protected abstract void OnMouseDown(object sender, MouseButtonEventArgs e);

        /// <summary> Set Type of Node - if Start/End Node change it in Environment. </summary>
        protected void SetType(Node node, NodeType type)
        {
            switch (node.Type)
            {
                case NodeType.Start:
                    StartNode = null;
                    break;
                case NodeType.End:
                    EndNode = null;
                    break;
            }
            switch (type)
            {
                case NodeType.Start:
                    if (StartNode != null) StartNode.SetDefaultType();
                    node.SetType(type);
                    StartNode = node;
                    break;
                case NodeType.End:
                    if (EndNode != null) EndNode.SetDefaultType();
                    node.SetType(type);
                    EndNode = node;
                    break;
                default:
                    node.SetType(type);
                    break;
            }  
        }

        /// <summary> All walls to empty nodes. </summary>
        public void ClearWalls() {
            foreach (Node node in Nodes) 
            { 
                if (node.Type == NodeType.Wall) node.SetType(NodeType.Empty);
            }
        }

        /// <summary> Set all nodes's State to <c>NodeState.Unseen</c>. </summary>
        public void ClearState() { foreach (Node node in Nodes) node.SetState(NodeState.Unseen); }

        /// <summary> Add path to canvas. </summary>
        public void AddPath(Path path)
        {
            path.Polyline.StrokeThickness = 4;  // change thickness of path
            Paths.Add(path);
            Canv.Children.Add(path.Polyline);
        }

        /// <summary> Remove all paths from canvas. </summary>
        public void RemovePaths()
        {
            foreach (Path path in Paths)
            {
                Canv.Children.Remove(path.Polyline);
            }
            Paths.Clear();
        }

        /// <summary> Remove everything from canvas. </summary>
        public void Clear()
        {
            Canv.Children.Clear();
        }
    }
}
