using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public class BanditsEventFactory : BaseEventFactory
    {
        public float setChanceForKidnap;
        [SerializeField] private GameObject banditsEventPrefab;
        public override BaseEvent CreateEvent(Vector3 position, float _rotation, Vector3 collidersSize, Transform spawnPointTransform)
        {
            //Instantiate Prefab in a Vector3 position, rotated around Y-axis by _rotation angles.
            GameObject instance = Instantiate(banditsEventPrefab.gameObject, position, spawnPointTransform.rotation);
            instance.transform.localScale = spawnPointTransform.localScale;

            //Set Colliders Size based on length and width only. Height is ignored.
            BoxCollider collider = instance.GetComponent<BoxCollider>();
            collider.size = collidersSize;

            BanditsEvent banditEvent = instance.GetComponent<BanditsEvent>();
            banditEvent.eventDuration = eventDurationToSet;
            banditEvent.chanceForKidnap = setChanceForKidnap;
            banditEvent.Initialise();

            return banditEvent;
        }
    }
}
