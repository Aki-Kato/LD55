using System.Collections;
using UnityEngine;
using WorldEvent;

public class FairEvent : BaseEvent
{
    public float eventReward;

    public override void EmployeeEnterEvent()
    {
        //Grant Gold to PlayerMoneyManager

        //Immediately Ends Event After
        EndEvent();
    }

    public override void EmployeeExitEvent()
    {
        throw new System.NotImplementedException();
    }

    protected override void EndEvent(){
        throw new System.NotImplementedException();
    }



}
