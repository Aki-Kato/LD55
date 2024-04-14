using System.Collections;
using Employees.Controllers;
using UnityEngine;
using WorldEvent;

public class FestivalEvent : BaseEvent
{
    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out EmployeeController employee))
        {
            //Slow Employee Code here...
            Debug.Log("Slow");

        }
    }

    public override void OnTriggerStay(Collider collider)
    {

    }

    public override void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out EmployeeController employee))
        {
            //Unslow Employee Code here...
            Debug.Log("Normal");
        }
    }

    protected override void EndEvent()
    {
        base.EndEvent();
    }
}
