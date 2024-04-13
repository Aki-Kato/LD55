using System.Collections.Generic;
using UnityEngine;

namespace Navigation.Graph
{
    public sealed class GraphController : MonoBehaviour
    {
        private LinkedList<GraphNode> _graphNodes = new LinkedList<GraphNode>();

        public void AddNode(GraphNode node)
        {
            if (_graphNodes.Count > 0)
            {
                var lastNode = _graphNodes.Last.Value;
                foreach (GraphNode activeNode in lastNode.NextNodes) 
                {
                    activeNode.gameObject.SetActive(false);
                }
            }

            _graphNodes.AddLast(node);
            node.SetNextNodes(true);
        }

        public void RemoveLastNode()
        {
            if (_graphNodes.Count == 0)
                return;

            var removedNode = _graphNodes.Last.Value;
            removedNode.SetNextNodes(false);
            _graphNodes.RemoveLast();

            var newLast = _graphNodes.Last.Value;
            newLast.SetNextNodes(true);
        }
    }
}

