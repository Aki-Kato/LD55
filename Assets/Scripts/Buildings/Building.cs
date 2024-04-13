using Navigation;
using UnityEngine;

namespace Buildings
{
    public sealed class Building : MonoBehaviour, INavigationFinalPoint
    {
        [SerializeField] private Transform buildingEntry;
        [SerializeField] private Mission currentMission;

        public Vector3 EntryPosition => buildingEntry.position;

        void Update(){
            //Timer for Mission
            currentMission.IncrementMissionTimer(Time.deltaTime);
        }

        public void SetMission(Mission mission){
            currentMission = mission;
        }

        
    }

}

