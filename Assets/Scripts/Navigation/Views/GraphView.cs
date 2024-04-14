using Navigation.Controllers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Navigation.Views
{
    public sealed class GraphView : AbstractPathSelectionView
    {
        [SerializeField] private GraphController graphController;

        protected override void OnEnable()
        {
            graphController.StartBuildingPath();
            resetButton.interactable = true;
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            graphController.StopBuildingPath();

            sendButton.interactable = false;
            resetButton.interactable = false;
            base.OnDisable();
        }

        private void Awake()
        {
            graphController.FinalNodeReached += GraphController_OnFinalNodeReached;
        }

        private void OnDestroy()
        {
            graphController.FinalNodeReached -= GraphController_OnFinalNodeReached;
        }

        private void GraphController_OnFinalNodeReached(bool reached)
        {
            sendButton.interactable = reached;
        }

        protected override void OnSendButtonClick()
        {
            sendButton.interactable = false;
            LinkedList<Vector3> points =
                new LinkedList<Vector3>(graphController.SelectedGraphNodes.Select(x => x.transform.position));

            _employeeController.SendBy(points);
            graphController.StopBuildingPath();

            OnPathCompleted();
        }

        protected override void OnResetButtonClick()
        {
            graphController.ClearAllNodes();
        }
    }
}
