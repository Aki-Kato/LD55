using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using Unity.Collections;
using UnityEngine;


public class MissionManager : MonoBehaviour
{
    public static MissionManager instance = null;
    private float timerToCreateNextMission;
    [SerializeField] private float missionCreationInterval = 20f;
    [SerializeField] private List<Building> listOfPossibleMissionLocations;
    private List<Building> listOfNonPossibleMissionLocations = new List<Building>();

    public event Action<Building> createdMission;
    public event Action<Building> completedMission;
    public event Action<Building> failedMission;

    [SerializeField] private MissionFactory missionGenerator;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CreateMission();
    }

    void Update()
    {
        TryCreateMission();
    }

    public void TryCreateMission()
    {
        timerToCreateNextMission += Time.deltaTime;

        //Timer Guard
        if (timerToCreateNextMission <= missionCreationInterval)
        {
            return;
        }

        //Possible Mission Locations Guard
        else if (listOfPossibleMissionLocations.Count > 0)
        {
            CreateMission();
        }
    }

    private void CreateMission()
    {
        timerToCreateNextMission = 0;

        //Select Random Location from a list of pre-determined Buildings positioned around the map
        Building _randomBuilding = listOfPossibleMissionLocations[UnityEngine.Random.Range(0, listOfPossibleMissionLocations.Count)];

        //GAME DESIGN ADJUSTMENT TO BE MADE HERE
        //Hardcoded numbers 2, 60 and 5 can be amended and randomised in accordance to an algorithm
        Mission _nextMission = missionGenerator.GenerateMissionAtBuilding(_randomBuilding);

        _randomBuilding.SetMission(_nextMission);

        UpdatePossibleMissionLocation(_randomBuilding, false);

        createdMission?.Invoke(_randomBuilding);
    }

    public void CompletedMission(Building building)
    {
        PlayerMoneyManager.instance.IncrementMoney(building.currentMission.reward);
        GameManager.instance.IncrementWinMission();

        UpdatePossibleMissionLocation(building, true);
        completedMission?.Invoke(building);
    }

    public void FailedMission(Building building)
    {
        GameManager.instance.IncrementLoseMission();

        UpdatePossibleMissionLocation(building, true);
        failedMission?.Invoke(building);
    }

    private void UpdatePossibleMissionLocation(Building building, bool ifAddingPotentialNewMissionLocation)
    {
        if (ifAddingPotentialNewMissionLocation)
        {
            listOfNonPossibleMissionLocations.Remove(building);
            listOfPossibleMissionLocations.Add(building);
        }

        else
        {
            listOfNonPossibleMissionLocations.Add(building);
            listOfPossibleMissionLocations.Remove(building);
        }
    }
}
