using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEvent
{
    public class AuctionEventFactory : BaseEventFactory
    {
        [SerializeField] private GameObject auctionEventPrefab;
        public override BaseEvent CreateEvent(Vector3 position,float _rotation, Vector3 collidersSize, Transform spawnPointTransform)
        {
            //Instantiate Prefab in a Vector3 position, rotated around Y-axis by _rotation angles.
            GameObject instance = Instantiate(auctionEventPrefab.gameObject, position, spawnPointTransform.rotation);
            instance.transform.localScale = spawnPointTransform.localScale;

            //Set Colliders Size based on length and width only. Height is ignored.
            BoxCollider collider = instance.GetComponent<BoxCollider>();
            collider.size = collidersSize;
            
            AuctionEvent festivalEvent = instance.GetComponent<AuctionEvent>();

            festivalEvent.eventDuration = eventDurationToSet;

            festivalEvent.Initialise();

            return festivalEvent;
        }
    }
}
