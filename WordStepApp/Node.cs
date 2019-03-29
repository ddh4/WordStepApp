using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStepApp
{
    public class Node
    {
        public readonly int id;
        public readonly string payload;
        public readonly LinkedList<Node> adjacent = new LinkedList<Node>();

        public Node(int id, string payload)
        {
            this.id = id;
            this.payload = payload;
        }
    }
}
