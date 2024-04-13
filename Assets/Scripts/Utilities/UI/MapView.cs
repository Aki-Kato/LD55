using UnityEngine;

namespace Utilities.UI
{
    public sealed class MapView : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private RectTransform rectTransform;

        public void TryGetCursorWorldCoordinates(out Ray? ray)
        {
            Vector2 position = rectTransform.InverseTransformPoint(Input.mousePosition);
            if (position.x < 0 || position.y < 0)
            {
                ray = null;
                return;
            }

            position.x /= rectTransform.rect.width;
            position.y /= rectTransform.rect.height;
            ray = cam.ViewportPointToRay(position);
        }
    }
}