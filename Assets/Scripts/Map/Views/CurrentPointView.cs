using Navigation.Controllers;
using System.Collections.Generic;
using UnityEngine;

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
            InitArrows();
        }

        private void OnDestroy()
        {
            graphController.NodeSelected -= GraphController_OnNodeSelected;
            graphController.GraphCleared -= GraphController_OnGraphCleared;
        }

        private void InitArrows()
        {
            foreach (var arrow in arrowViewsPool)
                arrow.Selected += Arrow_OnSelected;
        }

        private void Arrow_OnSelected(GraphNodeController graphNode)
        {
            graphController.AddNode(graphNode);
        }

        private void GraphController_OnNodeSelected(GraphNodeController node)
        {
            gameObject.SetActive(true);
            Vector2 position = map.GetWorldPositionOnMap(node.transform.position);
            transform.position = position;

            RotateArrowsTowardsNextNodes(node.NextNodes);
        }

        private void GraphController_OnGraphCleared()
        {
            gameObject.SetActive(false);
        }

        private void RotateArrowsTowardsNextNodes(List<GraphNodeController> nextNodes)
        {
            for (int i = 0; i < arrowViewsPool.Count; i++)
            {
                bool lessNodesThenArrows = i >= nextNodes.Count;
                arrowViewsPool[i].gameObject.SetActive(!lessNodesThenArrows);
                if (lessNodesThenArrows)
                    continue;

                var nextNode = nextNodes[i];
                Vector2 mapPosition = map.GetWorldPositionOnMap(nextNode.transform.position);
                arrowViewsPool[i].LookAt(mapPosition, nextNode);
            }
        }
    }
}

