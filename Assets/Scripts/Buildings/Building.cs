using Navigation;
using UnityEngine;

namespace Buildings
{
    public sealed class Building : MonoBehaviour, INavigationFinalPoint
    {
        [SerializeField] private Transform buildingEntry;
        public Mission currentMission = null;

        public Vector3 EntryPosition => buildingEntry.position;

        public void SetMission(Mission mission)
        {
            currentMission = mission;
            currentMission.SetMissionAvailability(true);
        }
        private void Update()
        {
            TryMissionFail();
        }
        private void TryMissionFail()
        {
            //Timer for Mission
            if (currentMission.ifMissionAvailable)
            {
                currentMission.IncrementMissionTimer(Time.deltaTime);

                if (currentMission.CurrentDuration >= currentMission.MissionDuration)
                {
                    FailMission();
                }
            }
        }


        /// <summary>
        /// When Employee has reached the mission successfully, either through TriggerEnter or others, run this function to appropriately contribute work units to the mission.
        /// If mission has reached final amount of work units, automatically calls events for winning mission.
        /// </summary>
        /// <param name="_workUnit"></param>
        public void ContributeToMission(int _workUnit)
        {
            Debug.Log($"Contributing to: " + currentMission.id);
            if (currentMission.ifMissionAvailable)
            {
                currentMission.IncrementWorkUnit(_workUnit);
                Debug.Log($"Contributed {_workUnit} workunits");
                if (currentMission.CurrentWorkDone >= currentMission.NumberOfWorkRequired)
                    CompleteMission();
            }
        }

        private void CompleteMission()
        {
            Debug.Log("Mission Complete");
            MissionManager.instance.CompletedMission(this);
            EndMission();
        }

        private void FailMission()
        {
            Debug.Log("Mission Fail");
            MissionManager.instance.FailedMission(this);
            EndMission();
        }

        private void EndMission()
        {
            currentMission.SetMissionAvailability(false);
        }
    }

}

