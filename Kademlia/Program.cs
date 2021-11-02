using System;

namespace Kademlia
{
    class Program
    {
        static void Main(string[] args)
        {
            int port;

            Console.Write("Enter the port of this node: ");
            port = Convert.ToInt32(Console.ReadLine());

            // https://docs.microsoft.com/de-de/dotnet/framework/network-programming/using-an-asynchronous-client-socket

            Node n = new Node(port);

            n.Listen(PrintRecvData);
        }

        static bool PrintRecvData(string data, System.Net.Sockets.NetworkStream stream)
        {
            Console.WriteLine(data);
            return true;
        }
    }
}
