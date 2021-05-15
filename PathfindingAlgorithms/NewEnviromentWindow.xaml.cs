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
    /// Interakční logika pro NewEnviromentWindow.xaml
    /// </summary>
    public partial class NewEnviromentWindow : Window
    {
        NewEnviromentViewModel VM;

        public EnviromentConstructor ReturnEnviroment;

        public NewEnviromentWindow()
        {
            InitializeComponent();

            VM = new NewEnviromentViewModel();
            DataContext = VM;

            VM.RowNum = 9;
            VM.ColNum = 16;

            VM.Radius = 70;
        }


        private void HexGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnEnviroment = new HexagonGridEnviromentConstructor()
            {
                Shape = (VM.ColNum, VM.RowNum)
            };
            DialogResult = true;
            this.Close();
        }
        private void PointGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnEnviroment = new PointEnviromentConstructor()
            {
                Shape = (VM.ColNum * 100, VM.RowNum * 100),
                Radius = VM.Radius
            };
            DialogResult = true;
            this.Close();
        }
        private void SquareGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnEnviroment = new SquareGridEnviromentConstructor()
            {
                Shape = (VM.ColNum, VM.RowNum)
            };
            DialogResult = true;
            this.Close();
        }
    }
}
