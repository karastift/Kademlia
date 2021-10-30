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

            Node n = new Node(port);

            n.StartApiListener();

            n.Ping(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(HelperFunctions.GetLocalIPAddress()), Convert.ToInt32(Console.ReadLine())));
        }
    }
}
