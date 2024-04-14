using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public class FestivalEventFactory : BaseEventFactory
    {
        [SerializeField] private GameObject festivalEventPrefab;
        public override BaseEvent CreateEvent(Vector3 position,float _rotation, Vector2 collidersSize)
        {
            //Instantiate Prefab in a Vector3 position, rotated around Y-axis by _rotation angles.
            GameObject instance = Instantiate(festivalEventPrefab.gameObject, position, Quaternion.Euler(0,_rotation,0));

            //Set Colliders Size based on length and width only. Height is ignored.
            BoxCollider collider = instance.GetComponent<BoxCollider>();
            collider.size = new Vector3(collidersSize.x, 5, collidersSize.y);

            FestivalEvent festivalEvent = instance.GetComponent<FestivalEvent>();

            festivalEvent.eventDuration = eventDurationToSet;

            festivalEvent.Initialise();

            return festivalEvent;
        }
    }
}
