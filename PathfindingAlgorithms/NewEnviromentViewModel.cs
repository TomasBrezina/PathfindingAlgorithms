using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfindingAlgorithms
{
    class NewEnviromentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private int lowerValue;
        public int LowerValue
        {
            get { return lowerValue; }
            set { lowerValue = value; OnPropertyChanged("LowerValue");}
        }
        private int higherValue;
        public int HigherValue
        {
            get { return higherValue; }
            set { higherValue = value; OnPropertyChanged("HigherValue"); }
        }
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
    }
}
