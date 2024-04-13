using Employees.Controllers;
using Navigation.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Map.Views;

namespace Navigation.Selector
{
    public sealed class NavigationPointsSelector : MonoBehaviour
    {
        public event Action PathCompleted;

        [SerializeField] private LayerMask selectionLayerMask;
        [SerializeField] private NavigationPointView selectedPointPrefab;

        [SerializeField] private MapView mapController;
        [SerializeField] private GraphController graphController;

        private EmployeeController _employeeController;

        public bool IsSelectionActive { get => enabled; set => enabled = value; }

        private void OnEnable()
        {
            graphController.StartBuildingPath();
        }

        private void OnDisable()
        {
            graphController.StopBuildingPath();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                TrySetPoint();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                graphController.RemoveLastNode();
            }
        }

        public void SetEmployeeForSelection(EmployeeController employeeController)
        {
            _employeeController = employeeController;
        }

        private void TrySetPoint()
        {
            mapController.TryGetCursorWorldCoordinates(out Ray? ray);
            if (!ray.HasValue)
                return;

            if (Physics.Raycast(ray.Value, out RaycastHit hitInfo, float.MaxValue, selectionLayerMask))
            {
                AddPathPoint(hitInfo);
            }
            else
            {
                Debug.LogWarning("There is no applicable surface for setting point.");
            }
        }

        private void AddPathPoint(RaycastHit hitInfo)
        {
            var selectedObject = hitInfo.collider.gameObject;
            if (selectedObject.TryGetComponent(out INavigationFinalPoint finalPoint))
            {
                CompleteThePath(finalPoint);
                return;
            }

            GraphNode graphNode = selectedObject.GetComponent<GraphNode>();
            graphController.AddNode(graphNode);
        }

        private void CompleteThePath(INavigationFinalPoint finalPoint)
        {
            LinkedList<Vector3> points =
                new LinkedList<Vector3>(graphController.SelectedGraphNodes.Select(x => x.transform.position));

            _employeeController.SendBy(points);
            graphController.StopBuildingPath();

            PathCompleted?.Invoke();
        }
    }
}
