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
        [SerializeField] private GraphView graphView;

        private void Awake()
        {
            summonSystem.sentEmployeeEvent += SummonSystem_OnSentEmployee;
            graphView.PathCompleted += PointsSelector_OnPathCompleted;
        }

        private void SummonSystem_OnSentEmployee(Employee employee)
        {
            EmployeeController controller = employeeControllerFactory.Create(employee);
            EnableSelectionMode(controller);
        }

        private void PointsSelector_OnPathCompleted()
        {
            graphView.IsSelectionActive = false;
        }

        private void EnableSelectionMode(EmployeeController controller)
        {
            graphView.SetEmployeeForSelection(controller);
            graphView.IsSelectionActive = true;
        }
    }
}