using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Navigation.Controllers
{
    public sealed class GraphController : MonoBehaviour
    {
        public event Action<GraphNodeController, List<GraphNodeController>> NodeSelected;
        public event Action GraphCleared;
        public event Action<bool> FinalNodeReached;

        [SerializeField] private GraphNodeController firstNode;
        [SerializeField] private LineRenderer lineRenderer;

        private LinkedList<GraphNodeController> _selectedGraphNodes = new LinkedList<GraphNodeController>();

        public LinkedList<GraphNodeController> SelectedGraphNodes => _selectedGraphNodes;

        public void StartBuildingPath()
        {
            AddNode(firstNode);
        }

        public void StopBuildingPath()
        {
            _selectedGraphNodes.Clear();
            lineRenderer.positionCount = 0;
            GraphCleared?.Invoke();
        }

        public void AddNode(GraphNodeController node)
        {
            _selectedGraphNodes.AddLast(node);
            var nextNotVisitedNodes = node.NextNodes.Except(_selectedGraphNodes).ToList();
            NodeSelected?.Invoke(node, nextNotVisitedNodes);

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position - node.transform.position);

            if (nextNotVisitedNodes.Count == 1)
                AddNode(nextNotVisitedNodes[0]);

            if (node.IsFinalNode)
                FinalNodeReached?.Invoke(true);
        }

        public void RemoveLastNode()
        {
            if (_selectedGraphNodes.Count == 0)
                return;

            GraphNodeController newLast;
            do
            {
                var removedNode = _selectedGraphNodes.Last.Value;
                _selectedGraphNodes.RemoveLast();
                if (removedNode.IsFinalNode)
                    FinalNodeReached?.Invoke(false);

                newLast = _selectedGraphNodes.Last.Value;
                NodeSelected?.Invoke(newLast, newLast.NextNodes);
                lineRenderer.positionCount--;
            }
            while (newLast.NextNodes.Count == 1 && _selectedGraphNodes.Count > 1);
        }

        public void ClearAllNodes()
        {
            while (_selectedGraphNodes.Count > 1)
            {
                _selectedGraphNodes.RemoveLast();
                lineRenderer.positionCount--;
            }

            FinalNodeReached?.Invoke(false);
            var lastNode = _selectedGraphNodes.Last.Value;
            NodeSelected?.Invoke(lastNode, lastNode.NextNodes);
        }
    }
}

