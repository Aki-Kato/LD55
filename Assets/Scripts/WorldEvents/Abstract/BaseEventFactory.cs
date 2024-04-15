using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public abstract class BaseEventFactory : MonoBehaviour
    {
        public float eventDurationToSet = 30f;
        public List<BaseEvent> listOfPositionsToCreateAt, listOfPositionsAlreadyCreatedAt;
        public abstract BaseEvent CreateEvent(BaseEvent _event);
        public abstract BaseEvent DestroyEvent(BaseEvent _event);

        void OnEnable()
        {
            EventManager eventManager = FindObjectOfType<EventManager>();
            eventManager.EventDestroyed += OnDestroyEvent;
        }

        void OnDisable()
        {
            EventManager eventManager = FindObjectOfType<EventManager>();
            eventManager.EventDestroyed -= OnDestroyEvent;

        }
        private void OnDestroyEvent(BaseEvent _event)
        {
            DestroyEvent(_event);
        }
        public BaseEvent SelectAvailablePosition()
        {
            //Select GameObject
            BaseEvent _event = listOfPositionsToCreateAt[Random.Range(0, listOfPositionsToCreateAt.Count)];
            UpdateAvailableEventLocation(_event, false);
            return _event;
        }

        protected virtual void UpdateAvailableEventLocation(BaseEvent _event, bool ifAdding)
        {
            if (_event != null)
            {
                if (ifAdding)
                {
                    listOfPositionsAlreadyCreatedAt.Remove(_event);
                    listOfPositionsToCreateAt.Add(_event);
                }

                else
                {
                    listOfPositionsAlreadyCreatedAt.Add(_event);
                    listOfPositionsToCreateAt.Remove(_event);
                }
            }
        }
    }
}
