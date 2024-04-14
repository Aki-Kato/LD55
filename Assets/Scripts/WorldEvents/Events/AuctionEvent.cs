using System.Collections;
using Employees.Controllers;
using UnityEngine;
using WorldEvent;

public class AuctionEvent : BaseEvent
{
    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out EmployeeController employee))
        {
            //Give Employee horse travel option
            employee.SetHorse();
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
        base.EndEvent();
    }
}
