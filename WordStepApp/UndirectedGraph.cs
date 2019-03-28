using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStepApp
{
    /// <summary>
    /// The Undirected Graph class.
    /// Contains all methods for performing algorithms specific to undirected graphs.
    /// </summary>
    /// <remarks>
    /// This class can perform BFS.
    /// </remarks>
    public class UndirectedGraph : Graph
    {

        /// <summary>
        /// Finds the shortest path between two nodes in the graph.
        /// </summary>
        /// <returns>
        /// A tuple containing a boolean (if a path existed) and a string list containing the path.
        /// </returns>
        public (bool, List<string>) HasPathBFS(Node source, Node destination)
        {
            Dictionary<int, int> parent = new Dictionary<int, int>();
            Dictionary<string, string> parentMapping = new Dictionary<string, string>();

            LinkedList<Node> nextToVisit = new LinkedList<Node>();
            HashSet<int> visited = new HashSet<int>();
            nextToVisit.AddLast(source);
            while (nextToVisit.Count > 0)
            {
                Node node = nextToVisit.First();
                nextToVisit.RemoveFirst();

                if (node.id == destination.id)
                {
                    return (true, null);
                }

                if (visited.Contains(node.id))
                {
                    continue;
                }

                visited.Add(node.id);

                foreach (Node child in node.adjacent)
                {
                    if (!visited.Contains(child.id) && !parentMapping.ContainsKey(child.payload))
                    {
                        parentMapping[child.payload] = node.payload;
                    }
                    // Can check here for destination - some time saving.
                    nextToVisit.AddLast(child);

                    if (child.id == destination.id)
                    {
                        return (true, GetPathPayload(parentMapping, source.payload, destination.payload));
                    }
                }
            }
            return (false, null);
        }

        /// <summary>
        /// Parses the path manifest to return the shortest path from the start and end node.
        /// </summary>
        /// <returns>
        /// A string list containing the shortest path between two nodes.
        /// </returns>
        protected List<string> GetPathPayload(Dictionary<string, string> parent, string start, string end)
        {
            List<string> path = new List<string>
            {
                end
            };
            while (path.Last() != start)
            {
                path.Add(parent[path.Last()]);
            }
            path.Reverse();
            return path;
        }
    }
}
