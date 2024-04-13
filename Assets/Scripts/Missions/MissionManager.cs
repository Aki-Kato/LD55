using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using UnityEngine;


public class MissionManager : MonoBehaviour
{
    public static MissionManager instance = null;
    private float timerToCreateNextMission;
    private static int numberOfMission = 0;
    [SerializeField] private float missionCreationInterval = 20f;
    [SerializeField] private List<Building> listOfPossibleMissionLocations;

    public event Action<Mission> createdMission;
    public event Action<Mission> completedMission;
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

    void Update()
    {
        TryCreateMission();
    }

    public void TryCreateMission()
    {
        //Timer Guard
        if (timerToCreateNextMission <= missionCreationInterval)
        {
            return;
        }

        else
        {
            timerToCreateNextMission = 0;

            //Select Random Location from a list of pre-determined Buildings positioned around the map
            int _rng = UnityEngine.Random.Range(0, listOfPossibleMissionLocations.Count);
            numberOfMission++;
            listOfPossibleMissionLocations[_rng].SetMission(new Mission(numberOfMission, 2, 60, 5));
        }
    }

}
