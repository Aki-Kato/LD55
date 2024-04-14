using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public class FestivalEventFactory : BaseEventFactory
    {
        public float eventRadiusToSet;
        [SerializeField] private FestivalEvent festivalEventPrefab;
        public override BaseEvent CreateEvent(Vector3 position)
        {
            GameObject instance = Instantiate(festivalEventPrefab.gameObject, position, Quaternion.identity);
            FestivalEvent festivalEvent = instance.GetComponent<FestivalEvent>();

            festivalEvent.eventDuration = eventDurationToSet;
            festivalEvent.eventRadius = eventRadiusToSet;

            festivalEvent.Initialise();

            return festivalEvent;
        }
    }
}
