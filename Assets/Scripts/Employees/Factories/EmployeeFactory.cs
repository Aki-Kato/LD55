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


    private List<PerkBase> InitialisePerksForEmployee()
    {
        //Random
        int _RNG = Random.Range(0, 3);
        List<PerkBase> _perks = new List<PerkBase>();
        for (int i = 0; i < _RNG; i++)
        {
            _perks.Add(allPerks[Random.Range(0, allPerks.Count)]);
        }

        return _perks;
    }

    public Employee GenerateNewEmployee()
    {
        Employee _newEmployee = new Employee
        {
            employeeName = NameGenerator.GenerateName(),

            //Algorithm for determining speed to be included here.
            baseRunSpeed = employeeBaseRunSpeed,

            model = modelsForEmployees[Random.Range(0, modelsForEmployees.Count)],

            listOfPerks = InitialisePerksForEmployee()
        };

        return _newEmployee;
    }
}
