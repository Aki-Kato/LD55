using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace WorldEvent
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager instance = null;

        [SerializeField] private float baseIntervalBetweenEvents;

        [Space]
        [SerializeField] private List<BaseEventFactory> listOfFactories;
        //Private Parameteres
        private float timerToNextEvent;
        private float intervalToCreateNewEvents;
        //Events
        public event Action<BaseEvent> EventCreated, EventDestroyed;

        void Awake()
        {
            if (instance == null)
                instance = this;

            else if (instance != this)
                Destroy(gameObject);

            intervalToCreateNewEvents = baseIntervalBetweenEvents;
        }

        void Start()
        {
        }
        void Update()
        {
            timerToNextEvent += Time.deltaTime;
            if (timerToNextEvent >= intervalToCreateNewEvents)
            {
                timerToNextEvent = 0;
                CreateNewEvent();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                DebugCreateNewEvent();
            }
        }

        private void UpdateIntervalBetweenEvents()
        {

        }



        public void CreateNewEvent()
        {
            Debug.Log("Creating Event");
            //Select random event
            BaseEvent _event = TryCreateEvent();

            if (_event != null)
                EventCreated?.Invoke(_event);
        }

        /// <summary>
        /// Go through all possible factories.
        /// 
        /// For each factory, go through all possible events that can spawn.
        /// If there are absolute no events that can spawn in the selected factory, redo and select another random factory.
        /// 
        /// If somehow, there are no possible factories available, then reset MissionManager duration for next mission
        /// </summary>
        /// <returns></returns>
        private BaseEvent TryCreateEvent()
        {
            #region To prevent infinite loop
            int _count = 0;
            #endregion

            //Select a random event, which if cannot be created, selects another one before finally selecting a unique one.
            BaseEventFactory _factory = listOfFactories[UnityEngine.Random.Range(0, listOfFactories.Count)];
            BaseEvent _event = _factory.SearchNonMutualExclusiveEventToCreate();

            //If it has repeated more than 10 times and still turned out non-unique, then stop.
            while (_event == null)
            {
                _event = listOfFactories[UnityEngine.Random.Range(0, listOfFactories.Count)].SearchNonMutualExclusiveEventToCreate();
                _count++;

                if (_count >= 10)
                {
                    //Somehow, somewhere, all the stars align and no factories (all events in all factories just don't work due to exclusive events) are available due to all the intersections
                    Debug.Log("Maxed Out");
                    timerToNextEvent = 0;
                    return null;
                }
            }

            return _event;
        }

        public void DestroyedEvent(BaseEvent _event)
        {
            EventDestroyed?.Invoke(_event);
        }

        public void DebugCreateNewEvent()
        {
            Debug.Log("Creating Event");
            CreateNewEvent();
        }
    }
}
