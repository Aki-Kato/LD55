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
            GameObject model = Instantiate(employee.employeeModel, controller.ModelRoot);
            model.transform.localPosition = new Vector3(0,-0.75f,0);

            //Instantiate Horse Model for Employees and disables them at Start
            GameObject horseModel = Instantiate(employee.horseModel, controller.ModelRoot);
            horseModel.name = "Horse";
            horseModel.transform.localPosition = new Vector3(0,-1,0);
            horseModel.SetActive(false);

            return controller;
        }
    }
}
