using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Navigation.Graph
{
    public sealed class GraphController : MonoBehaviour
    {
        public event Action<GraphNode> NodeSelected;
        public event Action GraphCleared;

        [SerializeField] private GraphNode firstNode;
        [SerializeField] private LineRenderer lineRenderer;

        private LinkedList<GraphNode> _selectedGraphNodes = new LinkedList<GraphNode>();

        public LinkedList<GraphNode> SelectedGraphNodes => _selectedGraphNodes;

        public void StartBuildingPath()
        {
            AddNode(firstNode);
        }

        public void StopBuildingPath()
        {
            _selectedGraphNodes.Clear();
            GraphCleared?.Invoke();
        }

        public void AddNode(GraphNode node)
        {
            if (_selectedGraphNodes.Count > 0)
            {
                var lastNode = _selectedGraphNodes.Last.Value;
                foreach (GraphNode activeNode in lastNode.NextNodes) 
                {
                    activeNode.gameObject.SetActive(false);
                }
            }

            _selectedGraphNodes.AddLast(node);
            node.SetNextNodes(true);
            NodeSelected?.Invoke(node);

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, node.transform.position);

            if (node.NextNodes.Count == 1)
                AddNode(node.NextNodes[0]);
        }

        public void RemoveLastNode()
        {
            if (_selectedGraphNodes.Count == 0)
                return;

            GraphNode newLast;
            do
            {
                var removedNode = _selectedGraphNodes.Last.Value;
                removedNode.SetNextNodes(false);
                _selectedGraphNodes.RemoveLast();

                newLast = _selectedGraphNodes.Last.Value;
                newLast.SetNextNodes(true);
                NodeSelected?.Invoke(newLast);
                lineRenderer.positionCount--;
            }
            while (newLast.NextNodes.Count == 1 && _selectedGraphNodes.Count > 1);
        }
    }
}

