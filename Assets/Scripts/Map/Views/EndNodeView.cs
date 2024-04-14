using Navigation.Controllers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Map.Views
{
    [RequireComponent(typeof(Button))]
    public sealed class EndNodeView : MonoBehaviour
    {
        public event Action<GraphNodeController> Selected;

        private GraphNodeController _graphNode;
        private Button _button;

        public GraphNodeController GraphNode => _graphNode;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void Construct(GraphNodeController graphNode, Vector3 position)
        {
            _graphNode = graphNode;
            transform.position = position;
        }

        private void OnClick()
        {
            Selected?.Invoke(_graphNode);
        }
    }
}