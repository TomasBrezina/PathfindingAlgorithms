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

        private string selectedEnviroment;
        public string SelectedEnviroment
        {
            get { return selectedEnviroment; }
            set { selectedEnviroment = value; OnPropertyChanged("SelectedEnviroment"); OnPropertyChanged("SelectedEnviromentImagePath"); }
        }
        public string SelectedEnviromentImagePath { 
            get
            {
                switch(SelectedEnviroment)
                {
                    case "Square grid":
                        return "images/background-square-grid.png";
                    case "Hexagon grid":
                        return "images/background-hexagon-grid.png";
                    default:
                        return "images/background-square-grid.png";
                }
            } 
        }
        public List<string> EnviromentList
        {
            get
            {
                OnPropertyChanged("SelectedEnviromentImagePath");
                return new List<string>()
                {
                    "Square grid",
                    "Hexagon grid"
                };
            }
            set { }
        }
    }
}
