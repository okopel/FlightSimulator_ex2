using FlightSimulator.Model;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;


namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {
        private flightManager flightManager;
        private bool _isSent { get; set; }
        public bool isSent{
            get
            {
                return _isSent;
            }
          private  set
            {
                _isSent = value;
                NotifyPropertyChanged("isSent");
            }
            }


        public AutoPilotViewModel()
        {
            // script in auto pilot text box 
            this.AutoScript = "Enter Instruction here :)";
            this.flightManager = flightManager.Instance;
            this.flightManager.PropertyChanged += makeWhightAfterSendingMsg;
        }
        // OK buttom in auto pilot
        private ICommand _okInAuto;
        public ICommand okInAuto
        {
            get
            {
                return _okInAuto ?? (_okInAuto = new Model.CommandHandler(() => okInAutoClick()));
            }
        }

        // send scripts
        private void okInAutoClick()
        {
            isSent = false;
            flightManager.SendScriptMSG(this.AutoScript);
        }
        //endOf okInAuto

        public string AutoScript
        {
            get; set;
        }

        // clear buttom in auto pilot
        private ICommand _clearInAuto;
        public ICommand clearInAuto
        {
            get
            {
                return _clearInAuto ?? (_clearInAuto = new Model.CommandHandler(() => clearInAutoClick()));
            }
        }
        private void clearInAutoClick()
        {
            this.AutoScript = "";
            NotifyPropertyChanged("AutoScript");
            isSent = false;
        }
        //endOf okInAuto

        private void makeWhightAfterSendingMsg(object o, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "cmdSent")
            {
                isSent = true;
            }
        }
    }
}
