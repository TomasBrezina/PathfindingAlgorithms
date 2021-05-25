using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    class NewEnvironmentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private int rowNum;
        public int RowNum
        {
            get { return rowNum; }
            set { rowNum = value; OnPropertyChanged("RowNum"); }
        }
        private int colNum;
        public int ColNum
        {
            get { return colNum; }
            set { colNum = value; OnPropertyChanged("ColNum"); }
        }
        private int radius;
        public int Radius
        {
            get { return radius; }
            set { radius = value; OnPropertyChanged("Radius"); }
        }
    }
}
