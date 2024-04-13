using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission
{
    public int id;
    private int currentWorkDone;
    [SerializeField] private int numberOfWorkRequired;
    private float currentDuration;
    [SerializeField] private float missionDuration;
    public float reward;
    public bool ifMissionAvailable = false;

    public int CurrentWorkDone => currentWorkDone;
    public int NumberOfWorkRequired => numberOfWorkRequired;
    public float CurrentDuration => currentDuration;
    public float MissionDuration => missionDuration;

    public Mission(int missionId, int numberOfWorkUnits, float missionDuration, float missionReward)
    {
        id = missionId;
        currentWorkDone = 0;
        numberOfWorkRequired = numberOfWorkUnits;
        this.missionDuration = missionDuration;
        reward = missionReward;
    }

    public void IncrementWorkUnit(int workUnit)
    {
        currentWorkDone += workUnit;
    }

    public void IncrementMissionTimer(float _time)
    {
        currentDuration += _time;
    }

    public void SetMissionAvailability(bool _state){
        ifMissionAvailable = _state;
    }
}
