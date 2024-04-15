using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public abstract class BaseEvent : MonoBehaviour
    {
        [HideInInspector] public float eventDuration;
        private float currentEventDuration;
        public abstract void OnTriggerEnter(Collider collider);
        public abstract void OnTriggerStay(Collider collider);
        public abstract void OnTriggerExit(Collider collider);


        void OnEnable()
        {
            //Must be added, otherwise there is error
            EventManager eventManager = FindObjectOfType<EventManager>();
            eventManager.EventCreated += OnCreateEvent;
        }

        void OnDisable()
        {
        }
        public virtual void Initialise()
        {
        }

        protected virtual void OnCreateEvent(BaseEvent baseEvent)
        {

        }
        void Update()
        {
            currentEventDuration += Time.deltaTime;
            if (currentEventDuration >= eventDuration)
            {
                EndEvent();
            }
        }

        protected virtual void OnDestroyEvent(BaseEvent baseEvent)
        {
        }

        protected virtual void EndEvent()
        {
            currentEventDuration = 0;
            EventManager.instance.DestroyedEvent(this);
            transform.parent.gameObject.SetActive(false);
        }


    }
}
