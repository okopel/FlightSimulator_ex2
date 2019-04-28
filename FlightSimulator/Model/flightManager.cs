using System;
using System.Threading;
using FlightSimulator.ViewModels;

namespace FlightSimulator.Model
{
    class flightManager :BaseNotify
    {
        private bool _isConnected;
        public bool isSending;
        private Client client = null;
        private Server server = null;
        string msgToSend = "";
        
        #region Singleton
        private static flightManager m_Instance = null;
        public static flightManager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new flightManager();
                }
                return m_Instance;
            }
        }
        #endregion
        private flightManager()
        {
            _isConnected = false;
            this.server = Server.Instance;
            this.client = Client.Instance;

        }
        
        public string MsgToSend { get => msgToSend; set => msgToSend = value; }

        // open server & client
        public void Connect()
        {
            // read params from config file
            string conf_ip = ApplicationSettingsModel.Instance.FlightServerIP;
            int conf_info_port = ApplicationSettingsModel.Instance.FlightInfoPort;
            int conf_command_port = ApplicationSettingsModel.Instance.FlightCommandPort;

            // if simulator already connected, close server & client,  then open again (with different params)
            if (_isConnected)
            {
                server.close();   
                client.close();
            }
            
            // open server on another thread
            Thread serverThread = new Thread(delegate ()
            {
                server.OpenServer(conf_ip, conf_info_port);
            });
            serverThread.Start();

            // open client on another thread
            Thread clientThread = new Thread(delegate ()
            {
                this.client.openClient(conf_ip, conf_command_port);
            });
            clientThread.Start();
        }

        /*
         * close the Server and the Client after btn push
         */
        public void DisConnect()
        {
            this.server.close();
            this.client.close();
        }

        /*
         for auto pilot mode:
         get long script, and send one command every 2 sec
        */
        public void SendScriptMSG(string msg)
        {
            this.isSending = true;
            Thread thread = new Thread(() =>
            {
                // make array of commands
                string[] lines = msg.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                for (int i = 0; i < lines.Length; i++)
                {
                    sendOneMSG(lines[i]);
                    // wait 2 sec
                    int timeToSleep = 2;
                    System.Threading.Thread.Sleep(timeToSleep * 1000);
                }
                this.isSending = false;
                NotifyPropertyChanged("cmdSent");
            });
            thread.Start();
        }

        // send message via client
        public void sendOneMSG(string msg)
        {
            if (this.client != null)
            {
                this.client.send(msg);
            }
        }
    }
}
