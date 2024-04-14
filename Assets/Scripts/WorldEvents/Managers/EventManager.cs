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
        [SerializeField] private List<GameObject> listOfSpawnPoints;

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
            if (timerToNextEvent >= intervalToCreateNewEvents && listOfSpawnPoints.Count > 0)
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
            EventSpawnPoint selectedSpawnPoint = GetRandomPositionOnMap();

            BaseEvent _event = listOfFactories[UnityEngine.Random.Range(0, listOfFactories.Count)].CreateEvent(selectedSpawnPoint.position, 0, selectedSpawnPoint.colliderSize);

            EventCreated?.Invoke(_event);
        }

        public void DestroyedEvent(BaseEvent _event)
        {
            Debug.Log("Recreating");
            //Recreate new EventSpawnPoint to add back into list - somehow is creating duplicates of gameObject SpawnPoint at Vector3.zero
            GameObject _newColliderGameObject = Instantiate(new GameObject("SpawnPoint"), _event.transform.position, Quaternion.identity);
            //Remove duplicate
            Destroy(GameObject.Find("SpawnPoint"));
            
            BoxCollider _collider = _newColliderGameObject.AddComponent<BoxCollider>();
            _collider.size = _event.gameObject.GetComponent<BoxCollider>().size;

            listOfSpawnPoints.Add(_newColliderGameObject);

            EventDestroyed?.Invoke(_event);
        }

        public void DebugCreateNewEvent()
        {
            Debug.Log("Creating Event");
            CreateNewEvent();
        }

        EventSpawnPoint GetRandomPositionOnMap()
        {
            EventSpawnPoint eventSpawnPoint = new EventSpawnPoint();
            GameObject selectedCollider = listOfSpawnPoints[UnityEngine.Random.Range(0, listOfSpawnPoints.Count)];
            listOfSpawnPoints.Remove(selectedCollider);
            Destroy(selectedCollider);

            eventSpawnPoint.position = selectedCollider.transform.position;
            eventSpawnPoint.colliderSize = selectedCollider.GetComponent<BoxCollider>().size;

            return eventSpawnPoint;
        }
    }
}
