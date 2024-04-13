using Navigation.Employee;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Navigation.Selector
{
    public sealed class NavigationPointsSelector : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private LayerMask selectionLayerMask;
        [SerializeField] private NavigationPointView selectedPointPrefab;

        [Space]
        // Only for testing purposes. Must be removed further.
        // Must be replace by Employee Controller or something like that.
        [SerializeField] private EmployeeAgent agent;

        private LinkedList<NavigationPointView> _selectedPoints = new LinkedList<NavigationPointView>();

        private NavigationPointView _lastSelectedPoint;

        private void Awake()
        {
            if (!cam)
                cam = Camera.main;

            CreateSelectedPoint(agent.transform.position);
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                TrySetPoint();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                DeleteSelectedPoint();
            }
        }

        private void TrySetPoint()
        {
            var mousePosition = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, selectionLayerMask))
            {
                var selectedObject = hitInfo.collider.gameObject;
                if (selectedObject.TryGetComponent(out INavigationFinalPoint finalPoint))
                {
                    CompleteThePath(finalPoint);
                    return;
                }

                CreateSelectedPoint(hitInfo.point);
            }
            else
            {
                Debug.LogWarning("There is no applicable surface for setting point.");
            }
        }

        private void CompleteThePath(INavigationFinalPoint finalPoint)
        {
            CreateSelectedPoint(finalPoint.EntryPosition);
            LinkedList<Vector3> points =
                new LinkedList<Vector3>(_selectedPoints.Select(x => x.transform.position));

            agent.SendBy(points);
            enabled = false;
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
