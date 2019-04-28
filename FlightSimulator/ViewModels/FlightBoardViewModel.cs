namespace FlightSimulator.ViewModels
{
    // Lon & Lat properties
    public class FlightBoardViewModel : BaseNotify
    {

        #region Singleton
        private static FlightBoardViewModel m_Instance = null;
        public static FlightBoardViewModel Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new FlightBoardViewModel();
                }
                return m_Instance;
            }
        }

        private FlightBoardViewModel() { }
        #endregion
        private double lon;

        public double Lon
        {
            get
            {
                return lon;
            }
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }

        private double lat;
        public double Lat
        {
            get
            {
                return lat;
            }
            set
            {
                lat = value;
              //  NotifyPropertyChanged("Lat");
            }
        }












    }
}
