using Employees.Controllers;
using Navigation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Navigation.Views
{
    public sealed class GraphView : MonoBehaviour
    {
        public event Action PathCompleted;

        [SerializeField] private GraphController graphController;
        [Space]
        [SerializeField] private Button sendButton;
        [SerializeField] private Button resetButton;

        private EmployeeController _employeeController;

        public bool IsSelectionActive { get => enabled; set => enabled = value; }

        private void OnEnable()
        {
            graphController.StartBuildingPath();
            resetButton.interactable = true;
        }

        private void OnDisable()
        {
            graphController.StopBuildingPath();
            sendButton.interactable = false;
            resetButton.interactable = false;
        }

        private void Awake()
        {
            graphController.FinalNodeReached += GraphController_OnFinalNodeReached;
            sendButton.onClick.AddListener(OnSendButtonClick);
            resetButton.onClick.AddListener(OnResetButtonClick);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                graphController.RemoveLastNode();
            }
        }

        public void SetEmployeeForSelection(EmployeeController employeeController)
        {
            _employeeController = employeeController;
        }

        private void GraphController_OnFinalNodeReached(bool reached)
        {
            sendButton.interactable = reached;
        }

        private void OnSendButtonClick()
        {
            sendButton.interactable = false;
            LinkedList<Vector3> points =
                new LinkedList<Vector3>(graphController.SelectedGraphNodes.Select(x => x.transform.position));

            _employeeController.SendBy(points);
            graphController.StopBuildingPath();

            PathCompleted?.Invoke();
        }

        private void OnResetButtonClick()
        {
            graphController.ClearAllNodes();
        }
    }
}
