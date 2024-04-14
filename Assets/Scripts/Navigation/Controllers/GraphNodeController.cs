using System.Collections.Generic;
using UnityEngine;

namespace Navigation.Controllers
{
    public sealed class GraphNodeController : MonoBehaviour
    {
        public List<GraphNodeController> NextNodes;

        public bool IsFinalNode => NextNodes.Count == 0;

        private void OnDrawGizmos()
        {
            if (NextNodes != null && NextNodes.Count > 0)
            {
                Gizmos.color = Color.red;
                foreach (var node in NextNodes)
                    Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
    }
}

