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
using System.Windows.Shapes;

namespace PathfindingAlgorithms
{
    /// <summary>
    /// Interakční logika pro NewEnvironmentWindow.xaml
    /// </summary>
    public partial class NewEnvironmentWindow : Window
    {
        NewEnvironmentViewModel VM;

        public EnvironmentConstructor ReturnEnvironment;

        public NewEnvironmentWindow()
        {
            InitializeComponent();

            VM = new NewEnvironmentViewModel();
            DataContext = VM;

            VM.RowNum = 9;
            VM.ColNum = 16;

            VM.Radius = 70;
        }


        private void HexGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnEnvironment = new HexagonGridEnvironmentConstructor()
            {
                Shape = (VM.ColNum, VM.RowNum)
            };
            DialogResult = true;
            this.Close();
        }
        private void PointGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnEnvironment = new PointEnvironmentConstructor()
            {
                Shape = (VM.ColNum * 100, VM.RowNum * 100),
                Radius = VM.Radius
            };
            DialogResult = true;
            this.Close();
        }
        private void SquareGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnEnvironment = new SquareGridEnvironmentConstructor()
            {
                Shape = (VM.ColNum, VM.RowNum)
            };
            DialogResult = true;
            this.Close();
        }
    }
}
