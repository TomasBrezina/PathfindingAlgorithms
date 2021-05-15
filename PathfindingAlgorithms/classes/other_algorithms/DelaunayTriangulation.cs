using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DelaunatorSharp;

namespace PathfindingAlgorithms
{ 
    public static class DelaunayTriangulation
    {
        public static List<(double, double, double, double)> GetEdges(List<Vector> points)
        {
            Delaunator DelaunayGenerator;

            IPoint[] Points = new IPoint[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                var pt = points[i];
                Points[i] = new DelaunatorSharp.Point(pt.X, pt.Y);
            }
            DelaunayGenerator = new Delaunator(Points);

            List<(double, double, double, double)> Edges = new List<(double, double, double, double)>();
            foreach (var edge in DelaunayGenerator.GetVoronoiEdges())
            {
                Edges.Add((edge.P.X, edge.P.Y, edge.Q.X, edge.Q.Y));
            };
            return Edges;
        } 
    }
}
