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
        public abstract void EmployeeEnterEvent();
        public abstract void EmployeeExitEvent();

        public event Action EventInitialise, EmployeeEnter, EmployeeExit, EventEnd;

        public virtual void Initialise(){
            EventInitialise?.Invoke();
        }   
        void Update(){
            currentEventDuration += Time.deltaTime;
            if (currentEventDuration >= eventDuration){
                EndEvent();
            }
        }

        protected abstract void EndEvent();

        
    }
}
