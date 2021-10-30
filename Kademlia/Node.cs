using System;

namespace Kademlia
{
    class Node : Api
    {
        public long Guid { set; get; }
        // routing table

        public Node(int port) : base(port)
        {
            // random number from 1 (excluding initial node) to 2^256 - 1
            this.Guid = new Random().Next(1, ((int) Math.Pow(2, 256)) - 1);
        }

    }
}
