using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using System.Windows.Controls;


namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Manual.xaml
    /// </summary>
    public partial class Manual : UserControl
    {
        private Client client;
        private ManuelViewModel vm;
        public Manual()
        {
            InitializeComponent();
            DataContext = this.vm=new ManuelViewModel();
        }
    }
}
