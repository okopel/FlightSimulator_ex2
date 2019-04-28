using FlightSimulator.Model;
using FlightSimulator.ViewModels.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class mapViewModel : BaseNotify
    {
        private Views.settingBox mySettingBox;
        private flightManager flightManager;

        public mapViewModel()
        {
            this.flightManager = flightManager.Instance;
        }

        ///Setting
        private ICommand _settingCommand;
        public ICommand SettingCommand
        {
            get
            {
                return _settingCommand ?? (_settingCommand = new Model.CommandHandler(() => SettingClick()));
            }
        }

        private SettingsWindowViewModel settingVM = null;
        // setting buttom
        private void SettingClick()
        {
            this.mySettingBox = new Views.settingBox();
            settingVM = new SettingsWindowViewModel(ApplicationSettingsModel.Instance);
            this.mySettingBox.setDataContex(settingVM);
            this.mySettingBox.ShowDialog();
        }

        // Connect buttom
        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new Model.CommandHandler(() => ConnectClick()));
            }

        }
        private void ConnectClick()
        {
            this.flightManager.Connect();
        }

        private ICommand _disConnectCommand;
        public ICommand DisConnectCommand
        {
            get
            {
                return _disConnectCommand ?? (_disConnectCommand = new Model.CommandHandler(() => DisConnectCommandClick()));
            }
        }


        private void DisConnectCommandClick()
        {
            this.flightManager.DisConnect();
        }
        //endOfConnect
    }
}
