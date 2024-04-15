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

        /// <summary>
        /// Debug code, for testing
        /// </summary>
        public Vector3 debugPosition;
        public float debugRotation;
        public Vector2 debugColliderSize;

        private float timerToNextEvent;
        [SerializeField] private float intervalToCreateNewEvents;

        [SerializeField] private List<BaseEventFactory> listOfFactories;

        public event Action<BaseEvent> EventCreated, EventDestroyed;

        void Awake()
        {
            if (instance == null)
                instance = this;

            else if (instance != this)
                Destroy(gameObject);
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

        public void CreateNewEvent()
        {
            Debug.Log("Creating Event");
            //Select random event
            BaseEventFactory eventFactory = listOfFactories[UnityEngine.Random.Range(0, listOfFactories.Count)];
            BaseEvent _event = eventFactory.CreateEvent(eventFactory.SelectAvailablePosition());
            
            if (_event != null)
                EventCreated?.Invoke(_event);
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
