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

        public void CreateNewEvent(){
            listOfFactories[Random.Range(0, listOfFactories.Count)].CreateEvent(GetRandomPositionOnMap());
        }

        Vector3 GetRandomPositionOnMap()
        {
            //////// PROTOTYPE CODE TO BE REPLACED
            return Vector3.zero;
            //////// PROTOTYPE CODE TO BE REPLACED

        }
    }
}
