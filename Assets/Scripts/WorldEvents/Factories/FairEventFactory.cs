using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public class FairEventFactory : BaseEventFactory
    {
        public int eventRewardToSet;
        [SerializeField] private GameObject fairEventPrefab;
        public override BaseEvent CreateEvent(Vector3 position,float _rotation, Vector3 collidersSize)
        {
            //Instantiate Prefab in a Vector3 position, rotated around Y-axis by _rotation angles.
            GameObject instance = Instantiate(fairEventPrefab.gameObject, position, Quaternion.Euler(0,_rotation,0));

            //Set Colliders Size based on length and width only. Height is ignored.
            BoxCollider collider = instance.GetComponent<BoxCollider>();
            collider.size = collidersSize;

            //Initialise Event Properties (for Fair Event)
            FairEvent fairEvent = instance.GetComponent<FairEvent>();
            fairEvent.eventDuration = eventDurationToSet;
            fairEvent.eventReward = eventRewardToSet;

            fairEvent.Initialise();

            return fairEvent;
        }

    }
}

