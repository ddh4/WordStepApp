using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace WordStepApp
{
    /// <summary>
    /// The Word Step Algorithm class.
    /// Contains all methods for solving the proposed word problem.
    /// </summary>
    /// <remarks>
    /// This class can load a dictionary from a txt file, generate a graph object and write the results to a txt file.
    /// </remarks>
    public class WordStepAlgorithm
    {
        private bool DictionaryLoaded = false;
        private bool GraphConstructed = false;

        private readonly string DictionaryPath;
        public string StartWord { get; set; }
        public string EndWord { get; set; }
        private readonly string ResultsPath;

        public HashSet<string> WordDictionary { get; private set; }
        UndirectedGraph WordGraph = new UndirectedGraph();

        /// <summary>
        /// Constructor to initiate the state of the class.
        /// </summary>
        public WordStepAlgorithm(string dictionaryPath, string startWord, string endWord, string resultsPath)
        {
            this.DictionaryPath = dictionaryPath;
            this.StartWord = startWord.ToLower();
            this.EndWord = endWord.ToLower();
            this.ResultsPath = resultsPath;
            WordDictionary = new HashSet<string>();
        }

        /// <summary>
        /// The algorithm steps to solve the word problem. 
        /// 1. Loads a dictionary file.
        /// 2. Constructs a graph.
        /// 3. Performs BFS.
        /// 4. Saves the result to a file.
        /// </summary>
        public void RunAlgorithm(int algorithm)
        {
            if (!DictionaryLoaded)
            {
                Console.WriteLine("\nLoading dictionary of valid words...");
                DictionaryLoaded = LoadDictionary();
            }
            if (!GraphConstructed && algorithm == 0)
            {
                Console.WriteLine("Building graph of valid words...");
                GraphConstructed = BuildWordGraph();
            }
            
            Console.Write("\nFinding word chain from {0} to {1} using ", StartWord, EndWord);

            (bool, List<string>) result = (false, null);

            switch (algorithm)
            {
                case 0:
                    Console.WriteLine("Algorithm 1 (complete graph)...");
                    result = CheckPathExists(StartWord, EndWord);
                    break;
                case 1:
                    Console.WriteLine("Algorithm 2 (dynamic graph)...");
                    WordGraph.ClearGraph();
                    result = CreateGraphBFS(StartWord, EndWord);
                    break;
                default:
                    Console.WriteLine("Algorithm 1 (complete graph)...");
                    result = CheckPathExists(StartWord, EndWord);
                    break;
            }

            Console.WriteLine("Generated graph contains {0} nodes.\n", WordGraph.NodeCount());
            Console.Write("Result: ");
            if (result.Item1)
            {
                result.Item2.ForEach(i => Console.Write(i.ToString()+" "));
                Console.WriteLine("\n\nSaving results to {0}", ResultsPath);
                SaveResults(result.Item2);
            }
            else
            {
                Console.WriteLine("No path exists for {0} and {1}.", StartWord, EndWord);
            }
        }

        /// <summary>
        /// Finds all legal word transformations of distance 1.
        /// </summary>
        /// <returns>
        /// A string list of direct transformations.
        /// </returns>
        public List<string> FindNeighbours(string source)
        {
            List<string> generatedWords = new List<string>();
            for (int i = 0; i < source.Length; i++)
            {
                var temp = source.ToCharArray();

                for (char c = 'a'; c <= 'z'; c++)
                {
                    temp[i] = c;
                    string newWord = new string(temp);
                    if (newWord == source) continue;
                    if (IsValidWord(newWord)) generatedWords.Add(newWord);
                }
            }
            return generatedWords;
        }

        /// <summary>
        /// Builds and finds the shortest path between two nodes in the graph.
        /// </summary>
        /// <returns>
        /// A tuple containing a boolean (if a path existed) and a string list containing the path.
        /// </returns>
        private (bool, List<string>) CreateGraphBFS(string source, string destination)
        {
            Dictionary<int, int> parent = new Dictionary<int, int>();
            Dictionary<string, string> parentMapping = new Dictionary<string, string>();

            LinkedList<Node> nextToVisit = new LinkedList<Node>();
            HashSet<int> visited = new HashSet<int>();
            int nodeId = 1;
            nextToVisit.AddLast(new Node(1, source));
            nodeId++;

            while(nextToVisit.Count > 0)
            {
                Node node = nextToVisit.First();
                nextToVisit.RemoveFirst();

                if (node.payload == destination)
                {
                    return (true, null);
                }
                if (visited.Contains(node.id))
                {
                    continue;
                }
                visited.Add(node.id);

                foreach (string neighbour in FindNeighbours(node.payload))
                {
                    var neighbourNode = WordGraph.GetNode(WordGraph.GetNodeId(neighbour));

                    if (neighbourNode == null)
                    {
                        WordGraph.AddNode(nodeId, new Node(nodeId++, neighbour));
                        neighbourNode = WordGraph.GetNode(WordGraph.GetNodeId(neighbour));
                    }

                    if (!visited.Contains(neighbourNode.id) && !parentMapping.ContainsKey(neighbourNode.payload))
                    {
                        parentMapping[neighbourNode.payload] = node.payload;
                    }

                    nextToVisit.AddLast(neighbourNode);

                    if (neighbourNode.payload == destination)
                    {
                        return (true, WordGraph.GetPathPayload(parentMapping, source, destination));
                    }
                }
            }

            return (false, null);
        }

        /// <summary>
        /// Loads a dictionary from a txt file.
        /// </summary>
        /// <returns>
        /// A boolean to flag whether the dictionary is loaded. 
        /// This prevents reloading if the algorithm is run again with different words.
        /// </returns>
        private bool LoadDictionary()
        {
            try
            {
                int validLines = 0;
                StreamReader reader = File.OpenText(this.DictionaryPath);
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if(line.Length == 4 && Regex.IsMatch(line, @"^[a-zA-Z]+$"))
                    {
                        WordDictionary.Add(line.ToLower());
                        validLines++;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Loads a graph object from the dictionary.
        /// </summary>
        /// <returns>
        /// A boolean to flag whether the graph is loaded. 
        /// This prevents reloading if the algorithm is run again with different words.
        /// </returns>
        private bool BuildWordGraph()
        {
            GenerateNodeList();
            GenerateEdges();
            return true;
        }

        /// <summary>
        /// Generates edges for all pairs of words that differ by 1 character.
        /// </summary>
        private void GenerateEdges()
        {
            foreach (var item in WordGraph.GetNodeList())
            {
                for (int i = 0; i < item.Value.payload.Length; i++)
                {
                    var temp = item.Value.payload.ToCharArray();

                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        temp[i] = c;
                        string newWord = new string(temp);
                        if (newWord == item.Value.payload) continue;

                        if (IsValidWord(newWord))
                        {
                            WordGraph.GetNodeId(newWord);
                            WordGraph.AddEdge(item.Value.id, WordGraph.GetNodeId(newWord));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generates nodes for all words in the dictionary.
        /// </summary>
        private void GenerateNodeList()
        {
            int id = 1;
            foreach (string word in WordDictionary)
            {
                WordGraph.AddNode(id, new Node(id, word));
                id++;
            }
        }

        /// <summary>
        /// Generates nodes for all words in the dictionary.
        /// </summary>
        /// <returns>
        /// A tuple containing a boolean, if a path exists and a string list containing the path.
        /// </returns>
        public (bool, List<string>) CheckPathExists(string start, string end)
        {
            if (IsValidWord(start) && IsValidWord(end))
            {
                int startNodeId = WordGraph.GetNodeId(start);
                int endNodeId = WordGraph.GetNodeId(end);
                return WordGraph.HasPathBFS(WordGraph.GetNode(startNodeId), WordGraph.GetNode(endNodeId));
            }
            else
            {
                return (false, null);
            }
        }

        /// <summary>
        /// Helper function to check whether a word is valid.
        /// </summary>
        /// <returns>
        /// A boolean value.
        /// </returns>
        private bool IsValidWord(string word)
        {
            return WordDictionary.Contains(word);
        }

        /// <summary>
        /// A function to save the string list results to a specified file.
        /// </summary>
        private void SaveResults(List<string> result)
        {
            System.IO.File.WriteAllLines(ResultsPath, result);
        }
    }
}