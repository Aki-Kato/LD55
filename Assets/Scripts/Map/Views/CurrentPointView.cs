using Navigation.Graph;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Map.Views
{
    public sealed class CurrentPointView : MonoBehaviour
    {
        [SerializeField] private GraphController graphController;
        [SerializeField] private MapView map;
        [SerializeField] private List<CurrentPointArrowView> arrowViewsPool;

        private void Awake()
        {
            graphController.NodeSelected += GraphController_OnNodeSelected;
            graphController.GraphCleared += GraphController_OnGraphCleared;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            graphController.NodeSelected -= GraphController_OnNodeSelected;
            graphController.GraphCleared -= GraphController_OnGraphCleared;
        }

        private void GraphController_OnNodeSelected(GraphNode node)
        {
            gameObject.SetActive(true);
            Vector2 position = map.GetWorldPositionOnMap(node.transform.position);
            transform.position = position;

            RotateArrowsTowardsNextNodes(node, node.NextNodes);
        }

        private void GraphController_OnGraphCleared()
        {
            gameObject.SetActive(false);
        }

        private void RotateArrowsTowardsNextNodes(GraphNode currentNode, List<GraphNode> nextNodes)
        {
            for (int i = 0; i < arrowViewsPool.Count; i++)
            {
                bool lessNodesThenArrows = i >= nextNodes.Count;
                arrowViewsPool[i].gameObject.SetActive(!lessNodesThenArrows);
                if (lessNodesThenArrows)
                    return;

                Vector2 mapPosition = GetFirstCornerOfPath(currentNode, nextNodes[i]);
                arrowViewsPool[i].LookAt(mapPosition);
            }
        }

        private Vector2 GetFirstCornerOfPath(GraphNode currentNode, GraphNode nextNode)
        {
            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(currentNode.transform.position, nextNode.transform.position, NavMesh.AllAreas, path);
            return map.GetWorldPositionOnMap(path.corners[1]);
        }
    }
}

