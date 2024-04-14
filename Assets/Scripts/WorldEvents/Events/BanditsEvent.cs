using System.Collections;
using Employees.Controllers;
using UnityEngine;
using WorldEvent;

public class BanditsEvent : BaseEvent
{
    [HideInInspector] public float chanceForKidnap = 50;
    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out EmployeeController employee))
        {
            float _RNG = Random.Range(0, 101);
            Debug.Log($"Current RNG: {_RNG}");
            if (_RNG <= chanceForKidnap)
            {
                Debug.Log($"Tried Kidnapping");

                //Chance to kidnap employee when crossing
                employee.TryKidnap();
            }
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
