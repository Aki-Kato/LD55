using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeFactory : MonoBehaviour
{

    [SerializeField] private List<GameObject> modelsForEmployees;
    public List<PerkBase> allPerks;

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
            speed = 3,

            model = modelsForEmployees[Random.Range(0, modelsForEmployees.Count)],

            listOfPerks = InitialisePerksForEmployee()
        };

        return _newEmployee;
    }
}
