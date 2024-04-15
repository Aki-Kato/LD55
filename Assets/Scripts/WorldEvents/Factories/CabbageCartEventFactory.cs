using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public class CabbageCartEventFactory : BaseEventFactory
    {
        public override BaseEvent CreateEvent(BaseEvent _event)
        {
            //Enable Event
            _event.transform.parent.gameObject.SetActive(true);

            //Set Properties of Events
            _event.eventDuration = eventDurationToSet;
            _event.Initialise();

            return _event;
        }

        public override BaseEvent DestroyEvent(BaseEvent _event)
        {
            UpdateAvailableEventLocation(_event as CabbageCartEvent, true);
            return _event;
        }
    }
}
