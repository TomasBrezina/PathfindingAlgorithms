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
        public Dictionary<string,Node> Nodes = new Dictionary<string,Node>();

        protected Canvas Canv;
        public Enviroment(Canvas canv)
        {
            Canv = canv;
        }
        public abstract void Initialize();
        public abstract void DrawPath(Node[] path);
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
        public override void Initialize()
        {
            RectHeight = Canv.ActualHeight / Shape[1];
            RectWidth = Canv.ActualWidth / Shape[0];

            for (int i = 0; i < Shape[0]; i++)
            {
                for (int j = 0; j < Shape[1]; j++)
                {
                    GridNode node = new GridNode(
                        $"{i},{j}",
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
                    Nodes.Add(node.ID, node);
                    Canv.Children.Add(node.Rect);
                    Canvas.SetLeft(node.Rect, (i * RectWidth));
                    Canvas.SetTop(node.Rect, (j * RectHeight));
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
                    string nodeKey = $"{i},{j}";
                    int[][] shifts = new int[4][] { new int[]{-1, 0}, new int[] { -1, -1}, new int[] { 0, -1}, new int[] { 1, -1}};
                    foreach (int[] shift in shifts)
                    {
                        int x = i + shift[0]; int y = j + shift[1];
                        if (x >= 0 && x < Shape[0] && y >= 0 && y < Shape[1])
                        {
                            Node thisNode = Nodes[$"{i},{j}"];
                            Node neighbourNode = Nodes[$"{x},{y}"];
                            thisNode.AddNeighbour(neighbourNode);
                            neighbourNode.AddNeighbour(thisNode);
                            DrawPath(new Node[] { thisNode, neighbourNode });
                        }
                    }
                }
            }
        }
        public override void DrawPath(Node[] path)
        {
            PointCollection pointsCollection = new PointCollection();
            foreach (var node in path)
            {
                pointsCollection.Add(new Point(
                    node.Pos[0],
                    node.Pos[1]
                ));
            }
            Polyline polyline = new Polyline();
            polyline.StrokeThickness = 1;
            polyline.Stroke = Brushes.Red;
            polyline.Points = pointsCollection;
            Canv.Children.Add(polyline);
        }
    }
}
