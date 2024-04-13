using System;
using System.Collections.Generic;
using UnityEngine;

namespace Navigation.Graph
{
    public sealed class GraphNode : MonoBehaviour
    {
        public List<GraphNode> NextNodes;

        public void SetNextNodes(bool active)
        {
            foreach (GraphNode node in NextNodes)
            {
                node.gameObject.SetActive(active);
            }

            gameObject.SetActive(!active);
        }
    }
}

