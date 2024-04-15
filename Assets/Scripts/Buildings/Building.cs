using Navigation;
using UnityEngine;
using Employees.Controllers;
using Unity.VisualScripting;

namespace Buildings
{
    public sealed class Building : MonoBehaviour
    {
        public int traderReachedDestinationExtraReward = 50;
        public Mission currentMission = null;
        public BuildingType buildingType;

        public AudioClip progress, success, coins;
        public GameObject soundPlayer;
        
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
            if (currentMission.ifMissionAvailable)
            {
                currentMission.IncrementWorkUnit(_workUnit);
                Debug.Log($"Contributed {_workUnit} workunits");
                if (currentMission.CurrentWorkDone >= currentMission.NumberOfWorkRequired)
                    CompleteMission();
                else
                {
                    var audio = Instantiate(soundPlayer).GetComponent<AudioSource>();
                    audio.volume = 0.4f;
                    audio.PlayOneShot(progress);
                }
            }
        }

        private void CompleteMission()
        {
            Debug.Log("Mission Complete");
            MissionManager.instance.CompletedMission(this);
            
            Instantiate(soundPlayer).GetComponent<AudioSource>().PlayOneShot(coins);
            
            var audio = Instantiate(soundPlayer).GetComponent<AudioSource>();
            audio.volume = 0.4f;
            audio.PlayOneShot(success);
            
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EmployeeController employee))
            {
                ContributeToMission(1 * employee._workContributionPerkModifier);

                //Check for Employee Trader Perk
                if (employee.IsTrader){
                    //PLACEHOLDER CODE - BALANCING REQUIRED
                    PlayerMoneyManager.instance.IncrementMoney(traderReachedDestinationExtraReward);
                }


                Destroy(employee.gameObject);
            }
        }
    }

    public enum BuildingType
    {
        University,
        Hospital,
        Teahouse,
        Palace,
    }
}

