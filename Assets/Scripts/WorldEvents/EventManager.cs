using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public class EventManager : MonoBehaviour
    {
        /// <summary>
        /// Debug code, for testing
        /// </summary>
        public Vector3 debugPosition;
        public float debugRotation;
        public Vector2 debugColliderSize;


        private float timerToNextEvent;
        [SerializeField] private float intervalToCreateNewEvents;

        [SerializeField] private List<BaseEventFactory> listOfFactories;
        void Update()
        {
            timerToNextEvent += Time.deltaTime;
            if (timerToNextEvent >= intervalToCreateNewEvents)
            {
                timerToNextEvent = 0;
                CreateNewEvent();
            }

            if (Input.GetKeyDown(KeyCode.P)){
                DebugCreateNewEvent();
            }
        }

        public void CreateNewEvent()
        {
            Debug.Log("Creating Event");

            ///////// PROTOTYPE CODE TO BE REPLACED
            listOfFactories[Random.Range(0, listOfFactories.Count)].CreateEvent(GetRandomPositionOnMap(), debugRotation, debugColliderSize);
            ///////// PROTOTYPE CODE TO BE REPLACED
        }

        public void DebugCreateNewEvent()
        {
            Debug.Log("Creating Event");

            ///////// PROTOTYPE CODE TO BE REPLACED
            listOfFactories[Random.Range(0, listOfFactories.Count)].CreateEvent(debugPosition, debugRotation, debugColliderSize);
            ///////// PROTOTYPE CODE TO BE REPLACED
        }

        Vector3 GetRandomPositionOnMap()
        {
            //////// PROTOTYPE CODE TO BE REPLACED
            return Vector3.zero;
            //////// PROTOTYPE CODE TO BE REPLACED

        }
    }
}
