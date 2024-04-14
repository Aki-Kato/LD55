using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldEvent;

public class FestivalEvent : BaseEvent
{
    public float eventRadius;

    public override void EmployeeEnterEvent()
    {
        //Employees move at half speed

        throw new System.NotImplementedException();
    }

    public override void EmployeeExitEvent()
    {
        //If Employees exit, make them move full speed
        throw new System.NotImplementedException();
    }

    protected override void EndEvent()
    {
        throw new System.NotImplementedException();
    }
}
