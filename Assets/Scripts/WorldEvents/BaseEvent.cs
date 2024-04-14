using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public abstract class BaseEvent : MonoBehaviour
    {
        public float eventDuration;
        private float currentEventDuration;
        public abstract void OnTriggerEnter(Collider collider);
        public abstract void OnTriggerStay(Collider collider);
        public abstract void OnTriggerExit(Collider collider);

        public event Action EventInitialise;

        public virtual void Initialise(){
            EventInitialise?.Invoke();
        }   
        void Update(){
            currentEventDuration += Time.deltaTime;
            if (currentEventDuration >= eventDuration){
                EndEvent();
            }
        }

        protected virtual void EndEvent(){
            Destroy(gameObject);
        }

        
    }
}
