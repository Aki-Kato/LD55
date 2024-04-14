using System.Collections.Generic;
using UnityEngine;

namespace Navigation.Graph
{
    public sealed class GraphNode : MonoBehaviour
    {
        public List<GraphNode> NextNodes;

        public bool IsFinalNode => NextNodes.Count == 0;

        public void SetNextNodes(bool active)
        {
            if (IsFinalNode)
                return;

            foreach (GraphNode node in NextNodes)
            {
                node.gameObject.SetActive(active);
            }

            gameObject.SetActive(!active);
        }

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

