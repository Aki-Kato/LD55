using UnityEngine;

namespace Map.Views
{
    public sealed class CurrentPointArrowView : MonoBehaviour
    {
        public void LookAt(Vector3 destination)
        {
            Vector3 rotateTowards = destination - transform.position;
            transform.rotation = Quaternion.LookRotation(transform.forward, rotateTowards);
        }
    }
}
