using Map.Views;
using System.Collections;
using UnityEngine;

namespace WorldEvent.Views
{
    public sealed class EventViewController : MonoBehaviour
    {
        [SerializeField] private int secondsToHide;
        [Space]
        [SerializeField] private RectTransform eventViewParent;
        [SerializeField] private EventView eventViewPrefab;

        [Header("View Data")]
        [SerializeField] private string eventName;
        [SerializeField, TextArea] private string eventDescription;

        private BaseEvent _eventController;
        private EventView _eventView;
        private Coroutine _showCoroutine;
        private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

        private void OnEnable()
        {
            _eventView.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _eventView.gameObject.SetActive(false);
            _eventView.Root.SetActive(false);
        }

        private void Awake()
        {
            _eventController = GetComponent<BaseEvent>();

            MapView map = FindObjectOfType<MapView>();
            Vector3 eventViewPosition = map.GetWorldPositionOnMap(_eventController.transform.position);
            _eventView = Instantiate(eventViewPrefab, eventViewParent);
            _eventView.transform.position = eventViewPosition;
            _eventView.Construct(eventName, eventDescription);
            _eventView.PointerOver += EventView_OnPointerOver;
        }

        private void OnDestroy()
        {
            _eventView.PointerOver -= EventView_OnPointerOver;
        }

        private void EventView_OnPointerOver()
        {
            ShowView();
        }

        private void ShowView()
        {
            if (_showCoroutine != null)
            {
                StopCoroutine(_showCoroutine);
            }

            _showCoroutine = StartCoroutine(ShowRoutine());
        }

        private IEnumerator ShowRoutine()
        {
            float time = secondsToHide;

            _eventView.Root.SetActive(true);
            while (time > 0)
            {
                int timeLeft = (int)(_eventController.eventDuration - _eventController.currentEventDuration);
                _eventView.SetTime(timeLeft);
                yield return _waitForEndOfFrame;
                time -= Time.deltaTime;
            }

            _eventView.Root.SetActive(false);
            _showCoroutine = null;
        }
    }
}
