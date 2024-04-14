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
    private static int numberOfMission = 0;
    [SerializeField] private float missionCreationInterval = 20f;
    [SerializeField] private List<Building> listOfPossibleMissionLocations;
    private List<Building> listOfNonPossibleMissionLocations = new List<Building>();

    public event Action<Mission> createdMission;
    public event Action<Mission> completedMission;
    public event Action<Mission> failedMission;
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
        numberOfMission++;

        //GAME DESIGN ADJUSTMENT TO BE MADE HERE
        //Hardcoded numbers 2, 60 and 5 can be amended and randomised in accordance to an algorithm
        Mission _nextMission = new Mission(numberOfMission, 3, 60, 5);
        _randomBuilding.SetMission(_nextMission);

        UpdatePossibleMissionLocation(_randomBuilding, false);

        createdMission?.Invoke(_nextMission);
    }

    public void CompletedMission(Building building)
    {
        PlayerMoneyManager.instance.IncrementMoney(building.currentMission.reward);
        completedMission?.Invoke(building.currentMission);
        UpdatePossibleMissionLocation(building, true);

    }

    public void FailedMission(Building building)
    {
        failedMission?.Invoke(building.currentMission);
        UpdatePossibleMissionLocation(building, true);
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
