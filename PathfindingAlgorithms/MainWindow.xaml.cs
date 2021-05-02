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
        Algorithm alg;
        DispatcherTimer stepTimer;
        MainViewModel MVM = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = MVM;
            stepTimer = new DispatcherTimer();
            stepTimer.Interval = TimeSpan.FromMilliseconds(20);
            stepTimer.Tick += new EventHandler(Step);

            Loaded += delegate
            {
                env = new Grid(GridCanvas, new int[] { 20, 10 });
                env.Initialize();   
            };
        }

        public void Step(object sender, EventArgs e)
        {
            Console.WriteLine(MainViewModel.SelectedNodeType);
            if (!alg.PathFound)
            {
                alg.Step(); 
            }
            else
            {
                Console.WriteLine();
                stepTimer.Stop();
            }
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (stepTimer.IsEnabled) stepTimer.Stop();
            else stepTimer.Start();
        }
        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            env.Clear();
            env.RemovePaths();

            Node startNode = env.Nodes[1];
            startNode.MarkAs(Brushes.Blue);
            Node endNode = env.Nodes[20];
            endNode.MarkAs(Brushes.Blue);
            alg = new BFS(startNode, endNode, env.Nodes.Count);
            env.AddPath(alg.Path);

            stepTimer.Start();
        }
    }

}
