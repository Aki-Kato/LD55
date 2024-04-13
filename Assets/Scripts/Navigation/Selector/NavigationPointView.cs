using UnityEngine;
using UnityEngine.AI;

namespace Navigation.Selector
{
    public sealed class NavigationPointView : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        public void SetNextPoint(Vector3 nextPoint)
        {
            NavMeshPath localPathPart = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, nextPoint, NavMesh.AllAreas, localPathPart);

            var corners = localPathPart.corners;
            lineRenderer.positionCount = corners.Length;
            lineRenderer.SetPositions(corners);            
        }

        public void RemoveNextPoint()
        {
            lineRenderer.positionCount = 0;
        }
    }
}
