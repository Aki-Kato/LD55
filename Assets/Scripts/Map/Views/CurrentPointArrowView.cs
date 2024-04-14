using Navigation.Graph;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Map.Views
{
    [RequireComponent(typeof(Button))]
    public sealed class CurrentPointArrowView : MonoBehaviour
    {
        public event Action<GraphNode> Selected;

        private Button _button;
        private GraphNode _graphNode;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            Selected?.Invoke(_graphNode);
        }

        public void LookAt(Vector3 destination, GraphNode node)
        {
            Vector3 rotateTowards = destination - transform.position;
            transform.rotation = Quaternion.LookRotation(transform.forward, rotateTowards);
            _graphNode = node;
        }
    }
}
