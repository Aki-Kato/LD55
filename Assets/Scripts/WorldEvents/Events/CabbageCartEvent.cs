using System.Collections;
using System.Collections.Generic;
using Employees.Controllers;
using UnityEngine;
using WorldEvent;

public class CabbageCartEvent : BaseEvent
{
    List<EmployeeController> stoppedEmployees = new List<EmployeeController>();
    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out EmployeeController employee))
        {
            //Check for Employee brute perk
            if (employee.IsBrute){
                EndEvent();
                return;
            }

            employee.SetCabbageCartSpeed(true);
            stoppedEmployees.Add(employee);
        }
    }

    public override void OnTriggerStay(Collider collider)
    {
    }

    public override void OnTriggerExit(Collider collider)
    {
        
    }

    protected override void EndEvent()
    {
        for (int i = 0 ; i < stoppedEmployees.Count; i++){
            stoppedEmployees[i].SetCabbageCartSpeed(false);
        }

        base.EndEvent();
    }
}
