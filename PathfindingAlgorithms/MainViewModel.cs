using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PathfindingAlgorithms
{

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private bool isRunning;
        public bool IsRunning {
            get { return isRunning; }
            set { isRunning = value; OnPropertyChanged("IsRunning"); }
        }

        private int selectedTickSpeed;
        public int SelectedTickSpeed
        {
            get { return selectedTickSpeed; }
            set { selectedTickSpeed = value; OnPropertyChanged("SelectedTickSpeed"); }
        }

        // Algorithms
        public string SelectedAlgorithm { get; set; }
        public List<string> AlgorithmsList
        {
            get { return Tools.AlgorithmsList; }
        }

        // Wall generators
        public string SelectedWallGenerator { get; set; }

        public List<string> WallGeneratorsList
        {
            get
            {
                return new List<string>()
                {
                    "Recursive subdivision",
                    "Noise generator",
                    "Clear walls"
                };
            }
        }

        // Node tools
        public static NodeType SelectedNodeType { get; set; }
        
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    public class ComparisonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value?.Equals(parameter);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value?.Equals(true) == true ? parameter : Binding.DoNothing;
        }
    }
}
