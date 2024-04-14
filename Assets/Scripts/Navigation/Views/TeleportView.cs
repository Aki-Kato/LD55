using Employees.Controllers;
using Map.Views;
using Navigation.Controllers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Navigation.Views
{
    public sealed class TeleportView : AbstractPathSelectionView
    {
        [SerializeField] private MapView map;
        [SerializeField] private List<GraphNodeController> finalGraphNodes;
        [SerializeField] private EndNodeView endNodeViewPrefab;

        private List<EndNodeView> _endNodeViews = new List<EndNodeView>();
        private GraphNodeController _selectedEndNode;

        protected override void OnEnable()
        {
            SetEndNodeViews(true);
            resetButton.interactable = true;
            sendButton.onClick.AddListener(OnSendButtonClick);
            resetButton.onClick.AddListener(OnResetButtonClick);
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            SetEndNodeViews(false);
            sendButton.interactable = false;
            resetButton.interactable = false;
            base.OnDisable();
        }

        private void Awake()
        {
            InitEndNodeViews();
        }

        private void SetEndNodeViews(bool active)
        {
            foreach (var view in _endNodeViews)
                view.gameObject.SetActive(active);
        }

        private void InitEndNodeViews()
        {
            foreach (var node in finalGraphNodes)
            {
                var endNodeView = Instantiate(endNodeViewPrefab, map.transform);
                var position = map.GetWorldPositionOnMap(node.transform.position);
                endNodeView.Construct(node, position);
                endNodeView.Selected += EndNodeView_OnSelected;
                _endNodeViews.Add(endNodeView);
                endNodeView.gameObject.SetActive(false);
            }
        }

        private void EndNodeView_OnSelected(GraphNodeController selectedEndNode)
        {
            foreach (var node in _endNodeViews.Where(x => x.GraphNode != selectedEndNode))
            {
                node.gameObject.SetActive(false);
            }

            _selectedEndNode = selectedEndNode;
            sendButton.interactable = true;
        }

        protected override void OnSendButtonClick()
        {
            _employeeController.CatapultTo(_selectedEndNode.transform.position);
            OnPathCompleted();
        }

        protected override void OnResetButtonClick()
        {
            sendButton.interactable = false;
            SetEndNodeViews(true);
        }
    }
}
