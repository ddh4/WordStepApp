using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStepApp
{
    /// <summary>
    /// The Graph class.
    /// Contains all methods for performing generic graph functions.
    /// </summary>
    /// <remarks>
    /// This class can perform can add nodes, retrieve nodes by id, payload or the entire (readonly) set.
    /// Additionally, the graph can be output to the console if used.
    /// </remarks>
    public class Graph
    {
        private Dictionary<int, Node> Nodes = new Dictionary<int, Node>();

        /// <summary>
        /// Clears a graph of nodes and associated edges.
        /// </summary>
        public void ClearGraph()
        {
            Nodes.Clear();
        }

        /// <summary>
        /// Returns the number of nodes in the graph.
        /// </summary>
        /// <returns>
        /// An integer count.
        /// </returns>
        public int NodeCount()
        {
            return Nodes.Count;
        }

        /// <summary>
        /// Allows a class to see node objects.
        /// </summary>
        /// <returns>
        /// A readonly dictionary of nodes.
        /// </returns>
        public IReadOnlyDictionary<int, Node> GetNodeList()
        {
            return Nodes;
        }

        /// <summary>
        /// A function to add nodes to the graph object.
        /// </summary>
        public void AddNode(int id, Node n)
        {
            try
            {
                Nodes.Add(id, n);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// A function to retrieve a node Id by querying for a payload.
        /// </summary>
        /// <returns>
        /// An integer Id if a node is found.
        /// </returns>
        public int GetNodeId(string payload)
        {
            return Nodes.FirstOrDefault(x => x.Value.payload == payload).Key;
        }

        /// <summary>
        /// A function to retrieve a node payload by querying for an id.
        /// </summary>
        /// <returns>
        /// An string payload if a node is found.
        /// </returns>
        public string GetNodePayload(int id)
        {
            return Nodes.FirstOrDefault(x => x.Value.id == id).Value.payload;
        }

        /// <summary>
        /// A function to retrieve a node object by id.
        /// </summary>
        /// <returns>
        /// A node object.
        /// </returns>
        public Node GetNode(int id)
        {
            try
            {
                return Nodes.FirstOrDefault(x => x.Value.id == id).Value;
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// A function to add an edge between two nodes ids.
        /// </summary>
        public void AddEdge(int source, int target)
        {
            Node s = GetNode(source);
            Node t = GetNode(target);
            s.adjacent.AddLast(t);
        }

        /// <summary>
        /// A function to write the graph object to the console for debugging.
        /// </summary>
        /// <remarks>
        /// Can be extended to output a graph file format and visualised in graph drawing software.
        /// </remarks>
        public void PrintGraph()
        {
            foreach (var node in GetNodeList())
            {
                Console.WriteLine("Node: {0}", node.Value.payload.ToString());
                Console.Write("Neighbors: ");
                foreach (var neighbour in node.Value.adjacent)
                {
                    Console.Write(neighbour.payload + ", ");
                }
                Console.WriteLine();
            }
        }
    }
}
