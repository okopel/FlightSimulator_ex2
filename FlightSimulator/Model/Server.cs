using FlightSimulator.ViewModels;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulator.Model
{
    class Server
    {

        #region Singleton
        private static Server m_Instance = null;
        public static Server Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Server();
                }
                return m_Instance;
            }
        }
        private Server()
        {
            _isConnected = false;
        }
        #endregion
        private bool _isConnected;
        private FlightBoardViewModel flight;
        // open server
        public void OpenServer(string ip, int infoPort)
        {
            flight = FlightBoardViewModel.Instance;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), infoPort);
            TcpListener listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for client connections...");
            TcpClient client = listener.AcceptTcpClient();
            _isConnected = true;
            Console.WriteLine("----- server connected -----");
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                while (_isConnected)
                {
                    bool isNewLine = false;
                    var charsData = new System.Text.StringBuilder();
                    char c;
                    while (!isNewLine)
                    {
                        //read until new line
                        c = reader.ReadChar();
                        switch (c)
                        {
                            case '\r':
                                //unix case
                                if (reader.PeekChar() == '\n')
                                {
                                    reader.ReadChar();
                                }
                                isNewLine = true;
                                break;
                            case '\n':
                                //win case
                                isNewLine = true;
                                break;
                            default:
                                //not new line case,so cintinu reading
                                charsData.Append(c);
                                break;
                        }
                    }
                    // get pocket of data from simulator
                    string data = charsData.ToString();
                    this.loadDataFromServer(data);
               //     System.Threading.Thread.Sleep(1 * 1000);
                }
            }
            client.Close();
            listener.Stop();
            Console.WriteLine("----- Server closed -----");
        }//end of open server

        // close server
        public void close()
        {
            _isConnected = false;
        }

        // get Lon & Lat
        private void loadDataFromServer(string data)
        {
            string[] args = data.Split(new[] { "," }, StringSplitOptions.None);
            flight.Lat = Convert.ToDouble(args[0]);
            flight.Lon = Convert.ToDouble(args[1]);
        }



    }
}

