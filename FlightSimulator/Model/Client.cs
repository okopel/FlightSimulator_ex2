using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FlightSimulator.Model
{
    // client, insert scripts to queue and send them to simulator
    class Client
    {
        private TcpClient client;
        private bool _isConnected;
        public Queue<string> queue;
        #region Singleton
        private static Client m_Instance = null;
        public static Client Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Client();
                }
                return m_Instance;
            }
        }
        private Client()
        {
            _isConnected = false;
            queue = new Queue<string>();
        }
        #endregion
        // script
        private string msg
        {
            get; set;
        }

        // open tcp client, using port, IP
        public void openClient(string ip, int CommandPort)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), CommandPort);
            client = new TcpClient();
            // try to connect
            while (!client.Connected)
            {
                try
                {
                    client.Connect(ep);
                }
                catch (Exception)
                {
                    System.Threading.Thread.Sleep(500);
                }
            }
            _isConnected = true;
            Console.WriteLine("----- client connected -----");

            // send stream
            Stream stm = client.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            Byte[] ba;
            Byte[] bb = new byte[100];
            while (_isConnected && client.Connected)
            {
                // send from queue
                while (this.queue.Count == 0)
                {
                    System.Threading.Thread.Sleep(500);
                }
                msg = queue.Dequeue();
                // append extention
                msg += "\r\n";
                ba = asen.GetBytes(msg);
                stm.Write(ba, 0, ba.Length);
                int k = stm.Read(bb, 0, 100);
                // get feedback
                /*Console.Write("feedback:");
                for (int i = 0; i < k; i++)
                {
                    Console.Write(Convert.ToChar(bb[i]));
                }*/
            }
            client.Close();
        }

        // public function
        // insern script to queue
        public void send(string msgToSend)
        {
            this.queue.Enqueue(msgToSend);
        }

        // close client
        public void close()
        {
            _isConnected = false;
            this.client.Close();
            if (!client.Connected)
            {
                Console.WriteLine("----- client closed -----");
            }
        }
    }


}
