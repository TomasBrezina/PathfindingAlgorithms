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
        Enviroment Env;
        Algorithm Alg;
        WallGenerator Gen;
        DispatcherTimer Timer;
        MainViewModel MVM = new MainViewModel();
        RunningState State;


        public MainWindow()
        {
            InitializeComponent();

            // Canvas
            GridCanvas.Width = 1280;
            GridCanvas.Height = 720;

            
            // Viev model            
            DataContext = MVM;
            MVM.IsRunning = false;

            // State
            State = RunningState.NotRunning;

            // Timer
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(15);
            Timer.Tick += new EventHandler(Step);

            Loaded += delegate
            {
                StartupInitialization();
            };
        }

        public void StartTimer(RunningState state)
        {
            Timer.Start();
            MVM.IsRunning = true;
            State = state;
        }
        public void StopTimer()
        {
            Timer.Stop();
            MVM.IsRunning = false;
            State = RunningState.NotRunning;
        }
        public void StartupInitialization()
        {

            //Env = new HexagonEnviroment(GridCanvas, new int[] { 16, 9 });
            Env = new GridEnviroment(GridCanvas, new int[] { 16, 9 });
            Env.StartNode = Env.Nodes.First();
            Env.EndNode = Env.Nodes.Last();
            Env.StartNode.SetType(NodeType.Start);
            Env.EndNode.SetType(NodeType.End);
        }
        public void Step(object sender, EventArgs e)
        {
            switch (State)
            {
                case RunningState.Algorithm:
                    if (!Alg.PathFound && Alg.PathExists != Exist.False) Alg.Step();
                    else StopTimer();
                    break;
                case RunningState.WallGenerator:
                    if (!Gen.IsFinished) Gen.Step();
                    else StopTimer();
                    break;
            }
        }
        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (Timer.IsEnabled) Timer.Stop();
            else Timer.Start();
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Env.ClearState();
            Env.RemovePaths();
            StopTimer();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (Env.StartNode != null && Env.EndNode != null)
            {
                Env.ClearState(); 
                Env.RemovePaths();
                Alg = Tools.AlgorithmFromString(MVM.SelectedAlgorithm, Env.StartNode, Env.Nodes);
                Env.AddPath(Alg.Path);
                StartTimer(RunningState.Algorithm);
            } else 
            {
                MessageBox.Show("Start and end have to be selected!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void GenerateWall_Click(object sender, RoutedEventArgs e)
        {
            Env.RemovePaths();
            Env.ClearState();
            Env.ClearWalls();

            //Gen = new NoiseWallGenerator(Env, 30);
            Gen = Tools.WallGeneratorFromString(MVM.SelectedWallGenerator, Env);
            StartTimer(RunningState.WallGenerator);
        }
    }

}
