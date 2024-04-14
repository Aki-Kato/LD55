using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public class EventManager : MonoBehaviour
    {
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
        }

        public void CreateNewEvent()
        {
            Debug.Log("Creating Event");

            ///////// PROTOTYPE CODE TO BE REPLACED
            listOfFactories[Random.Range(0, listOfFactories.Count)].CreateEvent(GetRandomPositionOnMap(), 45, new Vector2(2, 5));
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
