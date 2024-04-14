using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public class FairEventFactory : BaseEventFactory
    {
        public int eventRewardToSet;
        [SerializeField] private GameObject fairEventPrefab;
        public override BaseEvent CreateEvent(Vector3 position)
        {
            GameObject instance = Instantiate(fairEventPrefab.gameObject, position, Quaternion.identity);
            FairEvent fairEvent = instance.GetComponent<FairEvent>();

            fairEvent.eventDuration = eventDurationToSet;
            fairEvent.eventReward = eventRewardToSet;

            fairEvent.Initialise();

            return fairEvent;
        }
    }
}

