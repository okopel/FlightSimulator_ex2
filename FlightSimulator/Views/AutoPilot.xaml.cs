using FlightSimulator.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlightSimulator.Views.Windows
{
    /// <summary>
    /// Interaction logic for AutoPilot.xaml
    /// </summary>
    public partial class AutoPilot : UserControl
    {
        private AutoPilotViewModel vm;
        public AutoPilot()
        {
            // init
            InitializeComponent();
            DataContext =this.vm= new AutoPilotViewModel();

            // while script wasn't sent, textbox is pink
          //  this.colorfullTextBox.Background = Brushes.Pink;
       /*     if (vm.makeRed == null)
            {
                vm.makeRed = new Action(() => this.colorfullTextBox.Background = Brushes.Pink);
            }
            if (vm.makeWhite == null)
            {
                vm.makeWhite = new Action(() => this.colorfullTextBox.Background = Brushes.WhiteSmoke);
            }*/
        }
    }
}
