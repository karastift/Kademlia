using System;
using System.Net;
using System.Net.Sockets

namespace Kademlia
{
    class Api : TcpPeer
    {

        public Api(int port) : base(port) { }

        public void StartApiListener()
        {
            this.Listen(this.HandleMessage);
        }

        public bool HandleMessage(string message, string address, int port, NetworkStream stream)
        {
            string[] argv = message.Split(' ');

            switch (argv[0])
            {
                case "PING":
                    Console.WriteLine($"[+] Got pinged by {ep.Address}.");
                    this.Send("I got the fucking ping", ep.Address.ToString(), ep.Port);
                    break;
                default:
                    Console.WriteLine($"[!] Got invalid message via UDP '{message}'. Ignoring it.");
                    break;
            }
            return true;
        }

        public bool Ping(IPEndPoint ep)
        {
            // TODO

            return true;
        }
    }
}
