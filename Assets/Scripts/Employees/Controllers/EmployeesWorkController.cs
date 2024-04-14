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
            if ((travelOption & TravelOptions.GraphMovement) > 0)
            {
                EnableGraphMode(_lastSummonedEmployee);
            }
            else
            {
                EnableTeleportMode(_lastSummonedEmployee);
            }
        }

        private void SummonSystem_OnSentEmployee(Employee employee)
        {
            _lastSummonedEmployee = employeeControllerFactory.Create(employee);
        }

        private void GraphView_OnPathCompleted()
        {
            graphView.IsSelectionActive = false;
        }

        private void TeleportView_OnPathCompleted()
        {
            teleportView.IsSelectionActive = false;
        }

        private void EnableGraphMode(EmployeeController controller)
        {
            graphView.SetEmployeeForSelection(controller);
            graphView.IsSelectionActive = true;
        }

        private void EnableTeleportMode(EmployeeController controller)
        {
            teleportView.IsSelectionActive = true;
        }
    }
}