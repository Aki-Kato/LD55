using Navigation;
using UnityEngine;

namespace Buildings
{
    public sealed class Building : MonoBehaviour, INavigationFinalPoint
    {
        [SerializeField] private Transform buildingEntry;
        [SerializeField] private Mission currentMission;

        public Vector3 EntryPosition => buildingEntry.position;

        void Update()
        {
            //Timer for Mission
            currentMission.IncrementMissionTimer(Time.deltaTime);
        }

        public void SetMission(Mission mission)
        {
            currentMission = mission;
        }

        /// <summary>
        /// When Employee has reached the mission successfully, run this function to appropriately contribute work units to the mission.
        /// If mission has reached final amount of work units, automatically calls events for winning mission.
        /// </summary>
        /// <param name="_workUnit"></param>
        public void ContributeMission(int _workUnit)
        {
            currentMission.IncrementWorkUnit(_workUnit);
        }
    }

}

