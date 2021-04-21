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
    abstract class Enviroment
    {
        public List<Node> Nodes;
        protected List<Polyline> Paths;
        protected Canvas Canv;

        public Enviroment(Canvas canv)
        {
            Paths = new List<Polyline>();
            Nodes = new List<Node>();
            Canv = canv;
        }

        public abstract void Initialize();
        public abstract void Clear();
        public abstract int CoordsToIndex(int x, int y);
        public abstract void DrawPath(List<Node> path);
        public abstract void DrawPath(List<int> path);

        public void RemovePaths()
        {
            foreach (Polyline path in Paths)
            {
                Canv.Children.Remove(path);
            }
        }
    }

    class Grid : Enviroment
    {
        private double RectHeight;
        private double RectWidth;
        private int[] Shape;

        public Grid(Canvas canv, int[] shape) : base(canv)
        {
            Shape = shape;
        }
        public override int CoordsToIndex(int x, int y)
        {
            return x + (y * Shape[0]);
        }
        public override void Initialize()
        {
            RectHeight = Canv.ActualHeight / Shape[1];
            RectWidth = Canv.ActualWidth / Shape[0];
            
            int index = 0;
            for (int j = 0; j < Shape[1]; j++)
            {
                for (int i = 0; i < Shape[0]; i++)
                {
                    GridNode node = new GridNode(
                        index,
                        new double[2] { 
                            (i * RectWidth) + (RectWidth / 2), 
                            (j * RectHeight) + (RectHeight / 2) 
                        },
                        new Rectangle
                        {
                            Height = RectHeight,
                            Width = RectWidth,
                        }
                    );
                    Nodes.Add(node);
                    Canv.Children.Add(node.Rect);
                    Canvas.SetLeft(node.Rect, (i * RectWidth));
                    Canvas.SetTop(node.Rect, (j * RectHeight));
                    index += 1;
                }
            }
            ConnectNodes();
        }
        private void ConnectNodes()
        {
            for (int i = 0; i < Shape[0]; i++)
            {
                for (int j = 0; j < Shape[1]; j++)
                {
                    int[][] shifts = new int[4][] { new int[]{-1, 0}, new int[] { -1, -1}, new int[] { 0, -1}, new int[] { 1, -1}};
                    float[] weights = new float[] { 1, 1.4f, 1, 1.4f };
                    for (int shiftIndex = 0; shiftIndex < shifts.Length; shiftIndex++)
                    {
                        int x = i + shifts[shiftIndex][0]; int y = j + shifts[shiftIndex][1];
                        if (x >= 0 && x < Shape[0] && y >= 0 && y < Shape[1])
                        {
                            float weight = weights[shiftIndex];
                            Node thisNode = Nodes[CoordsToIndex(i, j)];
                            Node neighbourNode = Nodes[CoordsToIndex(x, y)];
                            thisNode.AddEdge(neighbourNode, weight);
                            neighbourNode.AddEdge(thisNode, weight);
                        }
                    }
                }
            }
        }
        public override void Clear()
        {
            foreach (Node node in Nodes) node.MarkAs(NodeBrushes.Default);
        }
        public override void DrawPath(List<Node> path)
        {
            PointCollection pointsCollection = new PointCollection();
            foreach (var node in path)
            {
                pointsCollection.Add(new Point(
                    node.Pos[0],
                    node.Pos[1]
                ));
            }
            Polyline polyline = new Polyline
            {
                StrokeThickness = 5,
                Stroke = Brushes.Black,
                Points = pointsCollection
            };
            Paths.Add(polyline);
            Canv.Children.Add(polyline);
        }
        public override void DrawPath(List<int> path)
        {
            List<Node> nodes = new List<Node>();
            foreach (int index in path) nodes.Add(Nodes[index]);
            DrawPath(nodes);
        }

    }
}
