using System.Collections;
using Employees.Controllers;
using UnityEngine;
using WorldEvent;

public class FairEvent : BaseEvent
{
    [HideInInspector] public int eventReward;

    
    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out EmployeeController employee))
        {
            //Grant Gold to PlayerMoneyManager
            PlayerMoneyManager.instance.IncrementMoney(eventReward);

            //Immediately Ends Event After
            EndEvent();
        }
    }

    public override void OnTriggerStay(Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit(Collider collider)
    {
        throw new System.NotImplementedException();
    }

    protected override void EndEvent()
    {
        base.EndEvent();
    }
}
