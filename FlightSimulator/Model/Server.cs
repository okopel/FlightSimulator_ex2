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
                string buffer = "";
                while (_isConnected)
                {
                    // get pocket of data from simulator
                    string data = reader.ReadString();
                    if (!(data.Contains("\r\n") || data.Contains("\n")))
                    {
                        buffer += data;
                        continue;
                    }
                    else
                    {
                        string[] parse = data.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                        buffer += parse[0];
                        // pocket finished 
                        this.loadDataFromServer(buffer);
                        // next pocket
                        buffer = parse[1];
                    }

                    System.Threading.Thread.Sleep(1 * 1000);
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
            try
            {
                flight.Lat = Convert.ToDouble(args[0]);
                flight.Lon = Convert.ToDouble(args[1]);
            }
            // fix strange bug
            catch
            {
                if (args[0].Length > 14)//6 digits after point*2+2 points+2* more than 1 digit before the .
                {
                    string[] del = args[0].Split('.');
                    string first = del[0] + "." + (del[1].Substring(0, 6));
                    string sec = (del[1].Substring(6)) + "." + del[2];

                    flight.Lat = Convert.ToDouble(first);
                    flight.Lon = Convert.ToDouble(sec);
                    return;
                }
            }
        }
    }
}
