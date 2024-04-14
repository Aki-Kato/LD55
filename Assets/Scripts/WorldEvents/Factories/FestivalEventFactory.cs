using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public class FestivalEventFactory : BaseEventFactory
    {
        [SerializeField] private GameObject festivalEventPrefab;
        public override BaseEvent CreateEvent(Vector3 position,float _rotation, Vector3 collidersSize)
        {
            //Instantiate Prefab in a Vector3 position, rotated around Y-axis by _rotation angles.
            GameObject instance = Instantiate(festivalEventPrefab, position, Quaternion.Euler(0,_rotation,0));

            //Set Colliders Size based on length and width only. Height is ignored.
            BoxCollider collider = instance.GetComponent<BoxCollider>();
            collider.size = collidersSize;

            FestivalEvent festivalEvent = instance.GetComponent<FestivalEvent>();
            festivalEvent.eventDuration = eventDurationToSet;
            festivalEvent.Initialise();

            return festivalEvent;
        }
    }
}
