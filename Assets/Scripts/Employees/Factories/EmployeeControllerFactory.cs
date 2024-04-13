using Employees.Controllers;
using UnityEngine;

namespace Employees.Factories
{
    public sealed class EmployeeControllerFactory : MonoBehaviour
    {
        [SerializeField] private EmployeeController controllerPrefab;
        [SerializeField] private Transform spawnPoint;

        public EmployeeController Create(Employee employee)
        {
            EmployeeController controller = Instantiate(controllerPrefab, spawnPoint.position, spawnPoint.rotation);
            controller.Initialise(employee.speed);
            return controller;
        }
    }
}
