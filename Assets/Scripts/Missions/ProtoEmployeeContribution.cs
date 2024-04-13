using System.Collections;
using System.Collections.Generic;
using Buildings;
using Navigation.Employee;
using UnityEngine;

public class ProtoEmployeeContribution : MonoBehaviour
{
    public EmployeeAgent employeeAgent;
    public int workUnit = 1;

    void OnEnable()
    {
        employeeAgent.FinalDestinationReached += ContributeToMission;
    }

    void OnDisable()
    {
        employeeAgent.FinalDestinationReached -= ContributeToMission;
    }

    public void ContributeToMission(EmployeeAgent employeeAgent)
    {
        //////PROTOTYPE CODE - TO REPLACE
        //Suggestion:
        //1. Obtain building data from finalDestinationReached event
        //2. Run the function obtained within Building.cs called ContributeToMission(workUnit)
        //workUnit is the int for the total contribution a worker has to a mission. Default is 1, with perks is 2 (refer to Miro);
        FindObjectOfType<Building>().ContributeToMission(workUnit);
        //////PROTOTYPE CODE - TO REPLACE
    }
}
