using System;
using System.Collections.Generic;
using UnityEngine;

namespace Navigation.Controllers
{
    public sealed class GraphController : MonoBehaviour
    {
        public event Action<GraphNodeController> NodeSelected;
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
            NodeSelected?.Invoke(node);

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, node.transform.position);

            if (node.NextNodes.Count == 1)
                AddNode(node.NextNodes[0]);

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
                NodeSelected?.Invoke(newLast);
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
            NodeSelected?.Invoke(_selectedGraphNodes.Last.Value);
        }
    }
}

