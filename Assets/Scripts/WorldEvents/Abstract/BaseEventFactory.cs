using System;
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

        public BaseEvent SearchNonMutualExclusiveEventToCreate()
        {
            BaseEvent _event = SelectAvailablePosition();
            if (_event != null)
                CreateEvent(_event);
            return _event;
        }

        private void OnDestroyEvent(BaseEvent _event)
        {
            DestroyEvent(_event);
        }
        public BaseEvent SelectAvailablePosition()
        {
            #region To prevent infinite loop
            int _count = 0;
            #endregion

            //Select a random event, which if cannot be created, selects another one before finally selecting a unique one.
            BaseEvent _event = listOfPositionsToCreateAt[UnityEngine.Random.Range(0, listOfPositionsToCreateAt.Count)];
            BaseEvent _fallbackEvent = null;

            //If it has repeated more than 10 times and still turned out non-unique, then stop.
            while (_event.IfCanCreateEvent() == false)
            {
                _event = listOfPositionsToCreateAt[UnityEngine.Random.Range(0, listOfPositionsToCreateAt.Count)];
                _count++;

                if (_count >= 10)
                {
                    _event = _fallbackEvent;
                    break;
                }
            }

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
