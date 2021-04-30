using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PathfindingAlgorithms
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Enviroment env;
        BFS alg;
        DispatcherTimer stepTimer;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += delegate
            {
                env = new Grid(GridCanvas, new int[] { 20, 10 });
                env.Initialize();   
            };
        }

        public void Step(object sender, EventArgs e)
        {
            if (alg.Step())
            {
                stepTimer.Stop();
                env.DrawPath(alg.FindPath());
            } 
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            env.Clear();
            env.RemovePaths();
            Node startNode = env.Nodes[env.CoordsToIndex(1, 1)];
            startNode.MarkAs(Brushes.Blue);
            Node endNode = env.Nodes[env.CoordsToIndex(10, 5)];
            endNode.MarkAs(Brushes.Blue);
            alg = new BFS(startNode, endNode, env.Nodes.Count);

            stepTimer = new DispatcherTimer();
            stepTimer.Interval = TimeSpan.FromMilliseconds(20);
            stepTimer.Tick += new EventHandler(Step);
            stepTimer.Start();
        }
    }
}
