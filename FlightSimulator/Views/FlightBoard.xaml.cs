using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using FlightSimulator.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class FlightBoard : UserControl
    {
        ObservableDataSource<Point> planeLocations = null;
        private FlightBoardViewModel vm;
        public FlightBoard()
        {
            InitializeComponent();
            vm = FlightBoardViewModel.Instance;
            vm.PropertyChanged += Vm_PropertyChanged;
            DataContext = vm;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);
            plotter.AddLineGraph(planeLocations, 2, "Route");
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName.Equals("Lat") || e.PropertyName.Equals("Lon"))
            {
                double x = this.vm.Lat;
                double y = this.vm.Lon;
                Point p1 = new Point(x,y);
                planeLocations.AppendAsync(Dispatcher, p1);
                System.Console.WriteLine(x+","+y);
            }
        }

    }

}

