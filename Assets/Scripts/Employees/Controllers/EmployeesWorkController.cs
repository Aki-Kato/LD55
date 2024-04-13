using Employees.Controllers;
using Employees.Factories;
using Navigation.Selector;
using UnityEngine;

public sealed class EmployeesWorkController : MonoBehaviour
{
    [Header("Game Controlling Systems")]
    [SerializeField] private SummonSystem summonSystem;
    [SerializeField] private EmployeeControllerFactory employeeControllerFactory;
    [SerializeField] private NavigationPointsSelector pointsSelector;

    private void Awake()
    {
        summonSystem.sentEmployeeEvent += SummonSystem_OnSentEmployee;
        pointsSelector.PathCompleted += PointsSelector_OnPathCompleted;
    }

    private void SummonSystem_OnSentEmployee(Employee employee)
    {
        EmployeeController controller = employeeControllerFactory.Create(employee);
        EnableSelectionMode(controller);
    }

    private void PointsSelector_OnPathCompleted()
    {
        pointsSelector.IsSelectionActive = false;
    }

    private void EnableSelectionMode(EmployeeController controller)
    {
        pointsSelector.SetEmployeeForSelection(controller);
        pointsSelector.IsSelectionActive = true;
    }
}
