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
            controller.Initialise(employee);

            //Instantiates Model for Employees with offset
            GameObject model = Instantiate(employee.model, controller.ModelRoot);
            model.transform.localPosition = new Vector3(0,-0.75f,0);

            return controller;
        }
    }
}
