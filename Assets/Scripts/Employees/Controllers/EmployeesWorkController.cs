using Employees.Enums;
using Employees.Factories;
using Navigation.Views;
using System;
using UnityEngine;

namespace Employees.Controllers
{
    public sealed class EmployeesWorkController : MonoBehaviour
    {
        public event Action<TravelOptions> TravelOptionUsed;

        [Header("Game Controlling Systems")]
        [SerializeField] private SummonSystem summonSystem;
        [SerializeField] private EmployeeControllerFactory employeeControllerFactory;
        [Space]
        [SerializeField] private GraphView graphView;
        [SerializeField] private TeleportView teleportView;

        private EmployeeController _lastSummonedEmployee;
        private TravelOptions _lastSelectedTravelOption;

        private void Awake()
        {
            summonSystem.sentEmployeeEvent += SummonSystem_OnSentEmployee;
            graphView.PathCompleted += GraphView_OnPathCompleted;
            teleportView.PathCompleted += TeleportView_OnPathCompleted;
        }

        private void OnDestroy()
        {
            summonSystem.sentEmployeeEvent -= SummonSystem_OnSentEmployee;
            graphView.PathCompleted -= GraphView_OnPathCompleted;
            teleportView.PathCompleted -= TeleportView_OnPathCompleted;
        }

        public void SelectTravelOption(TravelOptions travelOption)
        {
            graphView.IsSelectionActive = false;
            teleportView.IsSelectionActive = false;

            bool isGraphMovement = (travelOption & TravelOptions.GraphMovement) > 0;
            AbstractPathSelectionView view = isGraphMovement ? graphView : teleportView;

            _lastSummonedEmployee.SetTravelOption(travelOption);
            _lastSelectedTravelOption = travelOption;

            EnableMode(view, _lastSummonedEmployee);
        }

        private void SummonSystem_OnSentEmployee(Employee employee)
        {
            _lastSummonedEmployee = employeeControllerFactory.Create(employee);
        }

        private void GraphView_OnPathCompleted()
        {
            DisableMode(graphView);
        }

        private void TeleportView_OnPathCompleted()
        {
            DisableMode(teleportView);
        }

        private void DisableMode(AbstractPathSelectionView view)
        {
            view.IsSelectionActive = false;
            view.SetEmployeeForSelection(null);

            TravelOptionUsed?.Invoke(_lastSelectedTravelOption);
            _lastSelectedTravelOption = TravelOptions.Run;
        }

        private void EnableMode(AbstractPathSelectionView selector, EmployeeController controller)
        {
            selector.SetEmployeeForSelection(controller);
            selector.IsSelectionActive = true;
        }
    }
}