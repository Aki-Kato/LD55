using Employees.Enums;
using Employees.Factories;
using Navigation.Views;
using UnityEngine;

namespace Employees.Controllers
{
    public sealed class EmployeesWorkController : MonoBehaviour
    {
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

            EnableMode(view, _lastSummonedEmployee);
        }

        private void SummonSystem_OnSentEmployee(Employee employee)
        {
            _lastSummonedEmployee = employeeControllerFactory.Create(employee);
        }

        private void GraphView_OnPathCompleted()
        {
            graphView.IsSelectionActive = false;
            teleportView.SetEmployeeForSelection(null);
        }

        private void TeleportView_OnPathCompleted()
        {
            teleportView.IsSelectionActive = false;
            teleportView.SetEmployeeForSelection(null);
        }

        private void EnableMode(AbstractPathSelectionView selector, EmployeeController controller)
        {
            selector.SetEmployeeForSelection(controller);
            selector.IsSelectionActive = true;
        }
    }
}