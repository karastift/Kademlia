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

            n.StartApiListener();

            n.Ping(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(HelperFunctions.GetLocalIPAddress()), Convert.ToInt32(Console.ReadLine())));
        }
    }
}
