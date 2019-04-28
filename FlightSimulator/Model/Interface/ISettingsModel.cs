namespace FlightSimulator.Model.Interface
{
    public interface ISettingsModel
    {
        // connection DATA
        string FlightServerIP { get; set; }          // The IP Of the Flight Server
        int FlightInfoPort { get; set; }           // The Port of the Flight Server
        int FlightCommandPort { get; set; }           // The Port of the Flight Server

        void SaveSettings();
        void ReloadSettings();
    }
}
