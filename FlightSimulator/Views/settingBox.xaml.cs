using FlightSimulator.ViewModels.Windows;
using System;
using System.Windows;


namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for settingBox.xaml
    /// </summary>
    public partial class settingBox : Window
    {
         public settingBox()
        {
            InitializeComponent();
        }

        public void setDataContex(SettingsWindowViewModel vm)
        {
            DataContext = vm;
            if (vm.CloseAction == null)
            {
                vm.CloseAction = new Action(() => this.Hide());
            }
        }
     
    }
}
