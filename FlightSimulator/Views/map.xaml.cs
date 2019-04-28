using FlightSimulator.ViewModels;
using System.Windows.Controls;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for map.xaml
    /// </summary>
    public partial class map : UserControl
    {
        public map()
        {
            InitializeComponent();
            DataContext = new mapViewModel();
        }
    }
}
