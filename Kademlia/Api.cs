using System;
using System.Net;

namespace Kademlia
{
    class Api : UdpPeer
    {

        public Api(int port) : base(port) { }

        public void StartApiListener()
        {
            this.StartListener(this.HandleMessage);
        }

        public bool HandleMessage(string message, IPEndPoint ep)
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
            this.SendWithResponse("PING", ep);

            return true;
        }
    }
}
