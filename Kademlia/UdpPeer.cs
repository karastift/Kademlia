using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Kademlia
{
    class UdpPeer
    {
        public string Address { set; get; }
        // port should be constant, but for debugging purposes i have to make it dynamic
        // when i am debugging, the ports are the "addresses"
        public int Port;

        UdpClient client;
        Thread listener;

        public UdpPeer(int port)
        {
            this.Address = HelperFunctions.GetLocalIPAddress();
            this.Port = port;

            this.client = new UdpClient(new IPEndPoint(IPAddress.Parse(this.Address), port));
        }

        // sendport should be a constant but for debugging it is dynamic and the address is constant
        public void Send(string message, string address, int port)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            byte[] sendbuf = Encoding.ASCII.GetBytes(message);

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(address), port);
            
            Console.WriteLine($"[+] Sent '{message}' to '{address}' on port '{port}'.");

            s.SendTo(sendbuf, ep);
            s.Close();
        }

        public void StartListener(Func<string, IPEndPoint, bool> Callback)
        {
            this.listener = new Thread(() => Listen(Callback));
            this.listener.Start();
        }

        public void StopListener()
        {
            this.listener.Abort();
        }

        public void Listen(Func<string, IPEndPoint, bool> Callback)
        {
            // UdpClient listener = new UdpClient(this.Port);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, this.Port);

            try
            {
                while (true)
                {
                    byte[] bytes = client.Receive(ref groupEP);
                    string message = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                    Console.WriteLine("running1");
                    Callback(message, groupEP);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                client.Close();
            }
        }

        public string SendWithResponse(string message, IPEndPoint ep)
        {
            this.Send(message, ep.Address.ToString(), ep.Port);

            // UdpClient listener = new UdpClient(this.Port);

            try
            {
                while (true)
                {
                    byte[] bytes = client.Receive(ref ep);
                    string receivedMessage = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                    Console.WriteLine("running2");
                    Console.WriteLine(receivedMessage);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }

            return "";
        }
    }
}
