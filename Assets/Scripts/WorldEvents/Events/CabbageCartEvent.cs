using System.Collections;
using Employees.Controllers;
using UnityEngine;
using WorldEvent;

public class CabbageCartEvent : BaseEvent
{
    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out EmployeeController employee))
        {
            //Set Speed to Zero here
            Debug.Log("Slow");
        }
    }

    public override void OnTriggerStay(Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out EmployeeController employee))
        {
            //Return Speed back to Normal
            Debug.Log("Normal");
        }
    }

    protected override void EndEvent()
    {
        base.EndEvent();
    }
}
