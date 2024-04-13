using Employees.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities.UI;

namespace Navigation.Selector
{
    public sealed class NavigationPointsSelector : MonoBehaviour
    {
        public event Action PathCompleted;

        [SerializeField] private LayerMask selectionLayerMask;
        [SerializeField] private NavigationPointView selectedPointPrefab;
        [SerializeField] private MapView map;

        private EmployeeController _employeeController;

        private LinkedList<NavigationPointView> _selectedPoints = new LinkedList<NavigationPointView>();

        private NavigationPointView _lastSelectedPoint;

        public bool IsSelectionActive { get => enabled; set => enabled = value; }

        private void OnDisable()
        {
            ClearAllPoints();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                TrySetPoint();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                DeleteSelectedPoint();
            }
        }

        public void SetEmployeeForSelection(EmployeeController employeeController)
        {
            _employeeController = employeeController;
            CreateSelectedPoint(_employeeController.transform.position);
        }

        private void TrySetPoint()
        {
            map.TryGetCursorWorldCoordinates(out Ray? ray);
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

            CreateSelectedPoint(hitInfo.point);
        }

        private void CompleteThePath(INavigationFinalPoint finalPoint)
        {
            CreateSelectedPoint(finalPoint.EntryPosition);
            LinkedList<Vector3> points =
                new LinkedList<Vector3>(_selectedPoints.Select(x => x.transform.position));

            _employeeController.SendBy(points);
            PathCompleted?.Invoke();
        }

        private void CreateSelectedPoint(Vector3 point)
        {
            if (_lastSelectedPoint != null)
                _lastSelectedPoint.SetNextPoint(point);

            _lastSelectedPoint = Instantiate(selectedPointPrefab, point, Quaternion.identity);
            _selectedPoints.AddLast(_lastSelectedPoint);
        }

        private void DeleteSelectedPoint()
        {
            if (_selectedPoints.Count > 1)
            {
                _selectedPoints.RemoveLast();
                Destroy(_lastSelectedPoint.gameObject);

                _lastSelectedPoint = _selectedPoints.Last.Value;
                _lastSelectedPoint.RemoveNextPoint();
            }
        }

        private void ClearAllPoints()
        {
            while (_selectedPoints.Count != 0)
            {
                Destroy(_selectedPoints.Last.Value.gameObject);
                _selectedPoints.RemoveLast();
            }
        }
    }
}
