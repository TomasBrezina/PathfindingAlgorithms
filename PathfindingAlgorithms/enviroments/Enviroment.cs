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
    public abstract class Enviroment
    {
        public Node StartNode;
        public Node EndNode;

        public List<Node> Nodes;
        public int[] Shape;

        protected List<Path> Paths;
        protected Canvas Canv;

        public Enviroment(Canvas canv, int[] shape)
        {
            Paths = new List<Path>();
            Nodes = new List<Node>();
            Shape = shape;
            Canv = canv;
        }
        public abstract int CoordsToIndex(int x, int y);
        public abstract void Initialize();
        protected abstract void OnMouseDown(object sender, MouseButtonEventArgs e);
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
        public void ClearWalls() {
            foreach (Node node in Nodes) 
            { 
                if (node.Type == NodeType.Wall) node.SetType(NodeType.Empty);
            }
        }
        public void ClearState() { foreach (Node node in Nodes) node.SetState(NodeState.Unseen); }
        public void AddPath(Path path)
        {
            Paths.Add(path);
            Canv.Children.Add(path.Polyline);
        }
        public void RemovePaths()
        {
            foreach (Path path in Paths)
            {
                Canv.Children.Remove(path.Polyline);
            }
            Paths.Clear();
        }
    }
}
