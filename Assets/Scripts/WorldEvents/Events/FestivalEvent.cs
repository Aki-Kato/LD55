using System.Collections;
using Employees.Controllers;
using UnityEngine;
using WorldEvent;

public class FestivalEvent : BaseEvent
{
    public override void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Entered");
        if (collider.TryGetComponent(out EmployeeController employee))
        {
            employee.SetFestivalSpeed(true);
        }
    }

    public override void OnTriggerStay(Collider collider)
    {

    }

    public override void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out EmployeeController employee))
        {
            employee.SetFestivalSpeed(false);
        }
    }

    protected override void EndEvent()
    {
        base.EndEvent();
    }
}
