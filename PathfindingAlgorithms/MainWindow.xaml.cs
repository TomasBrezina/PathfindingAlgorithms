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

namespace PathfindingAlgorithms
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Enviroment env;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += delegate
            {
                env = new Grid(GridCanvas, new int[] { 30, 15 });
                env.Initialize();

                Node[] path = new Node[]
                {
                    env.Nodes["1,1"],
                    env.Nodes["2,1"],
                    env.Nodes["3,1"],
                    env.Nodes["4,1"],
                    env.Nodes["5,2"],
                    env.Nodes["1,2"],
                    env.Nodes["1,3"],
                };
                //env.DrawPath(path);
     
            };
        }
    }
}
