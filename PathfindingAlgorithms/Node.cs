using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PathfindingAlgorithms
{
    abstract class Node
    {
        public string ID;
        public double[] Pos;
        public List<Node> Neighbours;

        protected Node(string id, double[] pos)
        {
            ID = id;
            Pos = pos;
            Neighbours = new List<Node>();
        }
        public abstract void Mark();
        public void AddNeighbour(Node n)
        {
            Neighbours.Add(n);
        }
    }
    class GridNode : Node
    {
        public Rectangle Rect;
        public GridNode(string id, double[] pos, Rectangle rect) : base(id, pos)
        { 
            Rect = rect;
            Rect.Fill = Brushes.White;
            Rect.Stroke = Brushes.Gray;
            Rect.StrokeThickness = 0.5;
            Rect.MouseLeftButtonDown += Rect_MouseLeftButtonDown;
            Rect.MouseRightButtonDown += Rect_MouseRightButtonDown;
        }
        public override void Mark()
        {
            Rect.Fill = Brushes.Red;
        }

        private void Rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            rect.Fill = Brushes.Red;
        }

        private void Rect_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            rect.Fill = Brushes.White;
        }
    }
}
