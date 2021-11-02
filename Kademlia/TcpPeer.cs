using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Kademlia
{
    class TcpPeer
    {
        int Port { get; set; }

        public TcpPeer(int port) => Port = port;

        // sendport should be a constant but for debugging it is dynamic and the address is constant
        public void Send(string message, string address, int port)
        {
            try
            {
                TcpClient client = new TcpClient(address, port);

                byte[] buffer = Encoding.ASCII.GetBytes(message);

                NetworkStream stream = client.GetStream();

                stream.Write(buffer, 0, buffer.Length);

                Console.WriteLine($"[+] Sent '{message}' to '{address}' on port '{port}'.");

                buffer = new Byte[256];

                String responseData = String.Empty;

                Int32 bytes = stream.Read(buffer, 0, buffer.Length);
                responseData = Encoding.ASCII.GetString(buffer, 0, bytes);

                Console.WriteLine("[+] Received: {0}", responseData);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public void Send(string message, NetworkStream stream)
        { 
            try
            { 
                byte[] buffer = Encoding.ASCII.GetBytes(message);

                stream.Write(buffer, 0, buffer.Length);

                buffer = new Byte[256];

                String responseData = String.Empty;

                Int32 bytes = stream.Read(buffer, 0, buffer.Length);
                responseData = Encoding.ASCII.GetString(buffer, 0, bytes);

                Console.WriteLine("[+] Received: {0}", responseData);

                stream.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public void Listen(Func<string, string, int, NetworkStream, bool> Callback)
        {
            TcpListener server = null;

            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, Port);
                server.Start();

                Byte[] bytes = new Byte[256];
                String data = null;

                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    TcpClient client = server.AcceptTcpClient();

                    Console.WriteLine("Connected!");

                    data = null;

                    NetworkStream stream = client.GetStream();

                    int i;

                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = Encoding.ASCII.GetString(bytes, 0, i);

                        Console.WriteLine("[+] Received: {0}", data);

                        data = data.ToUpper();

                        // TODO get address and port of client to pass it to the callback function which logs it
                        Callback(data, client.Client);
                    }

                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
