using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public abstract class BaseEventFactory : MonoBehaviour
    {
        public float eventDurationToSet = 30f;
        public abstract BaseEvent CreateEvent(Vector3 position);
    }
}
