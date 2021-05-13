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

            VM.LowerValue = 1;
            VM.HigherValue = 3;
        }


        private void HexGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnEnviroment = new EnviromentConstructor()
            {
                EnvType = typeof(HexagonGridEnviroment),
                Shape = (VM.ColNum, VM.RowNum)
            };
            DialogResult = true;
            this.Close();
        }
        private void PointGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnEnviroment = new EnviromentConstructor()
            {
                EnvType = typeof(HexagonGridEnviroment),
                Shape = (VM.ColNum, VM.RowNum)
            };
            DialogResult = true;
            this.Close();
        }
        private void SquareGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            ReturnEnviroment = new EnviromentConstructor()
            {
                EnvType = typeof(SquareGridEnviroment),
                Shape = (VM.ColNum, VM.RowNum)
            };
            DialogResult = true;
            this.Close();
        }
    }
}
