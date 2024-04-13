using Employees.Controllers;
using UnityEngine;

namespace Employees.Factories
{
    public sealed class EmployeeControllerFactory : MonoBehaviour
    {
        [SerializeField] private EmployeeController controllerPrefab;
        [SerializeField] private Transform spawnPosition;

        public EmployeeController Create(Employee employee)
        {
            EmployeeController controller = Instantiate(controllerPrefab, spawnPosition);
            controller.Initialise(employee.speed);
            return controller;
        }
    }
}
