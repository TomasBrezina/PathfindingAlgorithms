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
        DispatcherTimer Timer;
        MainViewModel MVM = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = MVM;
            MVM.IsRunning = false;


            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromMilliseconds(20);
            Timer.Tick += new EventHandler(Step);

            Loaded += delegate
            {
                Env = new Grid(GridCanvas, new int[] { 20, 10 });
            };
        }

        public void Step(object sender, EventArgs e)
        {
            if (!Alg.PathFound)
            {
                Alg.Step();
            }
            else
            {
                Timer.Stop();
            }
        }
        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (Timer.IsEnabled) Timer.Stop();
            else Timer.Start();
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            MVM.IsRunning = false;
            Env.Clear();
            Env.RemovePaths();
            Timer.Stop();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(MVM.SelectedAlgorithm);

            if (Env.StartNode != null && Env.EndNode != null)
            {
                MVM.IsRunning = true;
                Env.Clear();
                Env.RemovePaths();
                Alg = Tools.AlgorithmFromString(MVM.SelectedAlgorithm, Env.StartNode, Env.Nodes);
                Env.AddPath(Alg.Path);
                Timer.Start();
            } else 
            {
                MessageBox.Show("Start and end have to be selected!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

}
