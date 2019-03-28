using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStepAppTest
{
    public class TestNode
    {
        public readonly int id;
        public readonly string payload;
        public readonly LinkedList<TestNode> adjacent = new LinkedList<TestNode>();

        public TestNode(int id, string payload)
        {
            this.id = id;
            this.payload = payload;
        }
    }
}
