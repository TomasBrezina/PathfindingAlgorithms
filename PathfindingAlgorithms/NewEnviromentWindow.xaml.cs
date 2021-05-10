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
    /// Interakční logika pro NewEnviroment.xaml
    /// </summary>
    public partial class NewEnviroment : Window
    {
        NewEnviromentViewModel VM;
        public NewEnviroment()
        {
            InitializeComponent();

            VM = new NewEnviromentViewModel();
            DataContext = VM;
        }
    }
}
