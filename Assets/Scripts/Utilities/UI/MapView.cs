using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities.UI
{
    public sealed class MapView : MonoBehaviour, IPointerMoveHandler
    {
        [SerializeField] private Camera cam;
        [SerializeField] private RectTransform rectTransform;

        private Vector2 _pointerLastPosition;

        public void TryGetCursorWorldCoordinates(out Ray? ray)
        {
            Vector2 position = rectTransform.InverseTransformPoint(_pointerLastPosition);
            if (position.x < 0 || position.y < 0)
            {
                ray = null;
                return;
            }

            position.x /= rectTransform.rect.width;
            position.y /= rectTransform.rect.height;
            ray = cam.ViewportPointToRay(position);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            _pointerLastPosition = eventData.position;
        }
    }
}