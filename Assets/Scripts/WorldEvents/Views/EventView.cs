using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WorldEvent.Views
{
    public sealed class EventView : MonoBehaviour, IPointerMoveHandler
    {
        public event Action PointerOver;

        [SerializeField] private GameObject root;
        [Space]
        [SerializeField] private TextMeshProUGUI eventNameLabel;
        [SerializeField] private TextMeshProUGUI eventDescriptionLabel;
        [SerializeField] private TextMeshProUGUI eventTimeLabel;

        public GameObject Root => root;

        public void OnPointerMove(PointerEventData eventData)
        {
            PointerOver?.Invoke();
        }

        public void Construct(string name, string description)
        {
            eventNameLabel.text = name;
            eventDescriptionLabel.text = description;
        }

        public void SetTime(int seconds)
        {
            TimeSpan timeLeft = TimeSpan.FromSeconds(seconds);
            eventTimeLabel.text = timeLeft.ToString(@"mm\:ss");
        }
    }
}

