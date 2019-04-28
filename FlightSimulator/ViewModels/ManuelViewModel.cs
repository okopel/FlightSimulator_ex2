using FlightSimulator.Model;

namespace FlightSimulator.ViewModels
{
    class ManuelViewModel : BaseNotify
    {
        private flightManager flightManager;
        public ManuelViewModel()
        {
            flightManager = flightManager.Instance;
        }

        private double _throttletxt;
        // set throttle by slider
        public double throttletxt
        {
            get { return _throttletxt; }
            set
            {
                _throttletxt = value;
                flightManager.sendOneMSG("set /controls/engines/current-engine/throttle " + value);
            }
        }

        private double _ruddertxt;
        // set rudder by slider
        public double ruddertxt
        {
            get { return _ruddertxt; }
            set
            {
                _ruddertxt = value;
                flightManager.sendOneMSG("set controls/flight/rudder " + value);
            }
        }
    }
}
