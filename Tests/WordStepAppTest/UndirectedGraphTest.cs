using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordStepApp;

namespace WordStepAppTest
{
    [TestClass]
    public class UndirectedGraphTest
    {

        [TestMethod]
        public void GetPathPayloadTestValid()
        {
            // Arrange
            UndirectedGraph testGraph = new UndirectedGraph();

            Dictionary<string, string[]> testData = new Dictionary<string, string[]>
            {
                {"bird", new string[] { "gird", "bard", "byrd", "bind" } },
                {"gird", new string[] { "bird", "gild", "girl", "girt" } },
                {"bard", new string[] { "card", "hard", "lard", "ward", "yard", "bird", "byrd", "bald", "band", "baud", "bawd", "barb", "bare", "bark", "barn", "barr", "bart" } },
                {"byrd", new string[] { "bard", "bird"}},
                {"bind", new string[] { "find", "hind", "kind", "lind", "mind", "wind", "band", "bend", "bond", "bird", "bing", "bini"}},
                {"wind", new string[] { "bind", "find", "hind", "kind", "lind", "mind", "wand", "wild", "wine", "wing", "wink", "wino", "winy"}},
                {"wing", new string[] { "bing", "ding", "king", "ping", "ring", "sing", "zing", "wang", "wong", "wind", "wine", "wink", "wino", "winy"}},
            };

            int nodeId = 1;
            foreach (string word in testData.Keys)
            {
                testGraph.AddNode(nodeId, new UndirectedGraph.Node(nodeId, word));
                nodeId++;
            }

            foreach(var node in testData)
            {
                foreach(string neighbour in node.Value)
                {
                    if (!testData.ContainsKey(neighbour))
                    {
                        continue;
                    }
                    else
                    {
                        testGraph.AddEdge(testGraph.GetNodeId(node.Key), testGraph.GetNodeId(neighbour));
                    }
                }
            }

            var expected = new List<string> { "bird", "bind", "wind", "wing" };

            // Act
            var actual = testGraph.HasPathBFS(testGraph.GetNode(testGraph.GetNodeId("bird")), testGraph.GetNode(testGraph.GetNodeId("wing"))); 
            
            // Assert
            Assert.IsTrue(actual.Item1);
            Assert.IsTrue(expected.Count == actual.Item2.Count);
            for(int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual.Item2[i]);
            }
        }


        [TestMethod]
        public void GetNodeTestValid()
        {
            // Arrange
            UndirectedGraph testGraph = new UndirectedGraph();
            testGraph.AddNode(1, new UndirectedGraph.Node(1, "spin"));
            int expectedId = 1;
            string expectedPayload = "spin";

            // Act
            var actual = testGraph.GetNode(1);

            // Assert
            Assert.AreEqual(expectedId, actual.id);
            Assert.AreEqual(expectedPayload, actual.payload);
        }

        [TestMethod]
        public void GetNodeTestInvalid()
        {
            // Arrange
            UndirectedGraph testGraph = new UndirectedGraph();
            testGraph.AddNode(1, new UndirectedGraph.Node(1, "spin"));
            UndirectedGraph.Node expected = null;

            // Act
            var actual = testGraph.GetNode(2);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetNodeIdTestValid()
        {
            // Arrange
            UndirectedGraph testGraph = new UndirectedGraph();
            testGraph.AddNode(1, new UndirectedGraph.Node(1, "spin"));
            int expectedId = 1;

            // Act
            var actual = testGraph.GetNodeId("spin");

            // Assert
            Assert.AreEqual(expectedId, actual);
        }

        [TestMethod]
        public void GetNodePayloadTestValid()
        {
            // Arrange
            UndirectedGraph testGraph = new UndirectedGraph();
            testGraph.AddNode(1, new UndirectedGraph.Node(1, "spin"));
            string expectedPayload = "spin";

            // Act
            var actual = testGraph.GetNodePayload(1);

            // Assert
            Assert.AreEqual(expectedPayload, actual);
        }

        [TestMethod]
        public void AddNodeTestInvalid()
        {
            // Arrange
            Graph testGraph = new UndirectedGraph();
            testGraph.AddNode(1, new UndirectedGraph.Node(1, "spin"));
            var expectedId = 1;
            var expectedPayload = "spin";

            // Act
            testGraph.AddNode(1, new UndirectedGraph.Node(1, "spot"));
            var actual = testGraph.GetNode(1);

            // Assert
            Assert.AreEqual(expectedId, actual.id);
            Assert.AreEqual(expectedPayload, actual.payload);
        }
    }
}
