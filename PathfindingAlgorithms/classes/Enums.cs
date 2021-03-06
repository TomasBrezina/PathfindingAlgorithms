using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PathfindingAlgorithms
{
    public enum RunningState
    {
        PathfindingAlgorithm,
        WallGenerator,
        NotRunning
    }
    public enum Exist
    {
        True,
        False,
        Unknown
    }
    public enum NodeType
    {
        Start,
        End,
        Empty,
        Wall,
    }
    public enum NodeState
    {
        Unseen,
        Revealed,
        Visited
    }
    public static class NodeBrushes
    {
        public static SolidColorBrush Start = Brushes.Green;
        public static SolidColorBrush End = Brushes.Red;
        public static SolidColorBrush Wall = Brushes.Black;
        public static SolidColorBrush Empty = Brushes.White;
        public static SolidColorBrush Visited = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999"));
        public static SolidColorBrush Revealed = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ccc"));

        public static Dictionary<NodeType, SolidColorBrush> FromType = new Dictionary<NodeType, SolidColorBrush> {
            {NodeType.Start, Start},
            {NodeType.End, End},
            {NodeType.Empty, Empty},
            {NodeType.Wall, Wall}
        };
        public static Dictionary<NodeState, SolidColorBrush> FromState = new Dictionary<NodeState, SolidColorBrush> {
            {NodeState.Unseen, Empty},
            {NodeState.Revealed, Revealed},
            {NodeState.Visited, Visited},
        };
    }
}
