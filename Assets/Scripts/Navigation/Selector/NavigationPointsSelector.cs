using Navigation.Employee;
using System.Collections.Generic;
using UnityEngine;

namespace Navigation.Selector
{
    public sealed class NavigationPointsSelector : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private LayerMask selectionLayerMask;
        [SerializeField] private Transform selectedPointPrefab;

        [Space]
        // Only for testing purposes. Must be removed further.
        // Must be replace by Employee Controller or something like that.
        [SerializeField] private EmployeeAgent agent;

        private LinkedList<Vector3> _selectedPoints = new LinkedList<Vector3>();

        private void Awake()
        {
            if (!cam)
                cam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                TrySetPoint();
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
                    CreateSelectedPoint(finalPoint.EntryPosition);
                    agent.SendBy(_selectedPoints);
                    return;
                }

                CreateSelectedPoint(hitInfo.point);
            }
            else
            {
                Debug.LogWarning("There is no applicable surface for setting point.");
            }
        }

        private void CreateSelectedPoint(Vector3 point)
        {
            _selectedPoints.AddLast(point);
            Instantiate(selectedPointPrefab, point, Quaternion.identity);
        }
    }
}
