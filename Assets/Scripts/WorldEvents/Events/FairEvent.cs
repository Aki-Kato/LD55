using System.Collections;
using Employees.Controllers;
using UnityEngine;
using WorldEvent;

public class FairEvent : BaseEvent
{
    [HideInInspector] public int eventReward;
    public AudioClip coins;
    public GameObject soundSource;
    
    public override void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out EmployeeController employee))
        {
            //Grant Gold to PlayerMoneyManager
            PlayerMoneyManager.instance.IncrementMoney(eventReward);

            Instantiate(soundSource).GetComponent<AudioSource>().PlayOneShot(coins);
            
            //Immediately Ends Event After
            EndEvent();
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
