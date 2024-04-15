using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeFactory : MonoBehaviour
{

    [SerializeField] private float employeeBaseRunSpeed = 3;
    [SerializeField] private float employeeeBaseHorseSpeed = 5;
    [SerializeField] private float employeeBaseCatapultSpeed = 20;
    [Space]
    public List<PerkBase> allPerks;
    [Space]
    [SerializeField] private List<GameObject> modelsForEmployees;
    [SerializeField] private GameObject horseModelforEmployee;


    private List<PerkBase> InitialisePerksForEmployee()
    {
        List<PerkBase> perks = new List<PerkBase>()
        {
            allPerks[Random.Range(0, allPerks.Count)]
        };

        return perks;
    }

    public Employee GenerateNewEmployee()
    {
        Employee _newEmployee = new Employee
        {
            employeeName = NameGenerator.GenerateName(),

            //Algorithm for determining speed to be included here.
            baseRunSpeed = employeeBaseRunSpeed,

            employeeModel = modelsForEmployees[Random.Range(0, modelsForEmployees.Count)],

            horseModel = horseModelforEmployee,

            listOfPerks = InitialisePerksForEmployee()
        };

        return _newEmployee;
    }
}
