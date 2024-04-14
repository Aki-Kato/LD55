using System.Collections;
using Employees.Controllers;
using UnityEngine;
using WorldEvent;

public class BanditsEvent : BaseEvent
{
    [HideInInspector] public float chanceForKidnap;
    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out EmployeeController employee))
        {
            if (Random.Range(0, 101) <= chanceForKidnap)
            {
                //Chance to kidnap employee when crossing
            }
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
