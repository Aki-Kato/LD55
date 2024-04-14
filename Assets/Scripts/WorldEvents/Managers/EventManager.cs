using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        [SerializeField] private GameObject AuctionSpawnPrefab, BanditSpanwPrefab, CabbageCartSpawnPrefab, FairSpawnPrefab, FestivalSpawnPrefab;
        [SerializeField] private List<GameObject> listOfAuctionSpawns, listOfBanditSpawns, listOfCabbageCartSpawns, listOfFairSpawns, listOfFestivalEventSpawns;

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
                TryCreateNewEvent();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                DebugCreateNewEvent();
            }
        }

        public void TryCreateNewEvent()
        {
            Debug.Log("Creating Event");

            BaseEventFactory baseEventFactory = listOfFactories[UnityEngine.Random.Range(0, listOfFactories.Count)];
            Debug.Log(baseEventFactory);
            EventSpawnPoint selectedSpawnPoint;
            switch (baseEventFactory)
            {
                case AuctionEventFactory:
                    selectedSpawnPoint = GetRandomPositionOnMap(listOfAuctionSpawns);
                    break;

                case BanditsEventFactory:
                    selectedSpawnPoint = GetRandomPositionOnMap(listOfBanditSpawns);

                    break;

                case CabbageCartEventFactory:
                    selectedSpawnPoint = GetRandomPositionOnMap(listOfCabbageCartSpawns);

                    break;

                case FairEventFactory:
                    selectedSpawnPoint = GetRandomPositionOnMap(listOfFairSpawns);

                    break;

                //If all else, use festival event spawn
                default:
                    selectedSpawnPoint = GetRandomPositionOnMap(listOfFestivalEventSpawns);
                    break;
            }

            BaseEvent _event = baseEventFactory.CreateEvent(selectedSpawnPoint.position, 0, selectedSpawnPoint.colliderSize, selectedSpawnPoint.transform);

            EventCreated?.Invoke(_event);
        }

        public void DestroyedEvent(BaseEvent _event)
        {
            Debug.Log("Recreating");
            //Recreate new EventSpawnPoint to add back into list - somehow is creating duplicates of gameObject SpawnPoint at Vector3.zero
            GameObject _newColliderGameObject;

            //Remove duplicate

            switch (_event)
            {
                case AuctionEvent:
                    _newColliderGameObject = Instantiate(AuctionSpawnPrefab, _event.transform.position, Quaternion.identity);
                    
                    _newColliderGameObject.transform.rotation = _event.transform.rotation;
                    _newColliderGameObject.transform.localScale = _event.transform.localScale;

                    _newColliderGameObject.GetComponent<BoxCollider>().size = _event.gameObject.GetComponent<BoxCollider>().size;
                    listOfAuctionSpawns.Add(_newColliderGameObject);
                    break;

                case BanditsEvent:
                    _newColliderGameObject = Instantiate(BanditSpanwPrefab, _event.transform.position, Quaternion.identity);
                    _newColliderGameObject.transform.rotation = _event.transform.rotation;
                    _newColliderGameObject.transform.localScale = _event.transform.localScale;

                    _newColliderGameObject.GetComponent<BoxCollider>().size = _event.gameObject.GetComponent<BoxCollider>().size;
                    listOfBanditSpawns.Add(_newColliderGameObject);
                    break;

                case CabbageCartEvent:
                    _newColliderGameObject = Instantiate(CabbageCartSpawnPrefab, _event.transform.position, Quaternion.identity);
                    _newColliderGameObject.transform.rotation = _event.transform.rotation;
                    _newColliderGameObject.transform.localScale = _event.transform.localScale;

                    _newColliderGameObject.GetComponent<BoxCollider>().size = _event.gameObject.GetComponent<BoxCollider>().size;
                    listOfCabbageCartSpawns.Add(_newColliderGameObject);
                    break;

                case FairEvent:
                    _newColliderGameObject = Instantiate(FairSpawnPrefab, _event.transform.position, Quaternion.identity);
                    _newColliderGameObject.transform.rotation = _event.transform.rotation;
                    _newColliderGameObject.transform.localScale = _event.transform.localScale;
                    _newColliderGameObject.GetComponent<BoxCollider>().size = _event.gameObject.GetComponent<BoxCollider>().size;
                    listOfFairSpawns.Add(_newColliderGameObject);
                    break;

                case FestivalEvent:
                    _newColliderGameObject = Instantiate(FestivalSpawnPrefab, _event.transform.position, Quaternion.identity);
                    _newColliderGameObject.transform.rotation = _event.transform.rotation;
                    _newColliderGameObject.transform.localScale = _event.transform.localScale;
                    _newColliderGameObject.GetComponent<BoxCollider>().size = _event.gameObject.GetComponent<BoxCollider>().size;
                    listOfFestivalEventSpawns.Add(_newColliderGameObject);
                    break;

            }


            EventDestroyed?.Invoke(_event);
        }

        public void DebugCreateNewEvent()
        {
            Debug.Log("Creating Event");
            TryCreateNewEvent();
        }

        EventSpawnPoint GetRandomPositionOnMap(List<GameObject> _listOfSpawnPoints)
        {
            EventSpawnPoint eventSpawnPoint = new EventSpawnPoint();
            GameObject selectedCollider = _listOfSpawnPoints[UnityEngine.Random.Range(0, _listOfSpawnPoints.Count)];
            Debug.Log($"Selected Collider: {selectedCollider.name}");
            _listOfSpawnPoints.Remove(selectedCollider);
            Destroy(selectedCollider);

            eventSpawnPoint.transform = selectedCollider.transform;
            eventSpawnPoint.position = selectedCollider.transform.position;
            eventSpawnPoint.colliderSize = selectedCollider.GetComponent<BoxCollider>().size;

            return eventSpawnPoint;
        }
    }
}
