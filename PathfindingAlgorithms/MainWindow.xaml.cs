using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
using System.ComponentModel;

namespace PathfindingAlgorithms
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel MVM = new MainViewModel(); 
        private Main M; // Main class

        public MainWindow()
        {
            InitializeComponent();

            // Canvas
            GridCanvas.Width = 680;
            GridCanvas.Height = 480;
   
            // Viev model            
            DataContext = MVM;
            MVM.IsRunning = false;
            MVM.SelectedTickSpeed = 3;

            // Main class
            M = new Main(GridCanvas, MVM); 

            // Wait for the window to load
            Loaded += delegate
            {
                M.StartupInitialization();
            };
        }
        
        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            M.PlayPauseTimer();
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            M.StopTimerAndClear();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (M.IsStart) M.Start(MVM.SelectedAlgorithm);
            else MessageBox.Show("Start and end have to be selected!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void GenerateWall_Click(object sender, RoutedEventArgs e)
        {
            M.GenerateWall(MVM.SelectedWallGenerator);
        }
        private void EnvironmentNewButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new NewEnvironmentWindow();
            if (win.ShowDialog() == true) M.NewEnvironment(win.ReturnEnvironment);
        }
        private void EnvironmentResetButton_Click(object sender, RoutedEventArgs e)
        {
            M.ResetEnvironment();
        }
        private void EnvironmentExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new About();
            win.Show();
        }
        private void SourceButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/TomasBrezina/PathfindingAlgorithms");
        }

        private void AlgorithmInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var link = Tools.AlgorithmToLink(MVM.SelectedAlgorithm);
            if (link != null) Process.Start(link);
        }

        // change color of toolbar overflow button
        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            Brush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#eee"));
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as Grid;
            if (overflowGrid != null) overflowGrid.Background = brush;
            var overflowButton = toolBar.Template.FindName("OverflowButton", toolBar) as ToggleButton;
            if (overflowButton != null) overflowButton.Background = brush;
            var overflowPanel = toolBar.Template.FindName("PART_ToolBarOverflowPanel", toolBar) as ToolBarOverflowPanel;
            if (overflowPanel != null) overflowPanel.Background = brush;
        }
        private void TickSpeedSlider_ValueChange(object sender, RoutedEventArgs e)
        {
            int intv = MVM.SelectedTickSpeed;
            if (intv > 0 && intv <= 16) { M.ChangeTimerInterval(intv*intv); }
        }
        
    }

}
