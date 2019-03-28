using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordStepApp;

namespace WordStepAppTest
{
    [TestClass]
    public class GraphTest
    {

        [TestMethod]
        public void GetNodeTestValid()
        {
            // Arrange
            Graph testGraph = new Graph();
            testGraph.AddNode(1, new Graph.Node(1, "spin"));
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
            Graph testGraph = new Graph();
            testGraph.AddNode(1, new Graph.Node(1, "spin"));
            Graph.Node expected = null;

            // Act
            var actual = testGraph.GetNode(2);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetNodeIdTestValid()
        {
            // Arrange
            Graph testGraph = new Graph();
            testGraph.AddNode(1, new Graph.Node(1, "spin"));
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
            Graph testGraph = new Graph();
            testGraph.AddNode(1, new Graph.Node(1, "spin"));
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
            Graph testGraph = new Graph();
            testGraph.AddNode(1, new Graph.Node(1, "spin"));
            var expectedId = 1;
            var expectedPayload = "spin";

            // Act
            testGraph.AddNode(1, new Graph.Node(1, "spot"));
            var actual = testGraph.GetNode(1);

            // Assert
            Assert.AreEqual(expectedId, actual.id);
            Assert.AreEqual(expectedPayload, actual.payload);
        }
    }
}
