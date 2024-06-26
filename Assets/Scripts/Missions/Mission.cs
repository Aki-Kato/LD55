using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission
{
    public static int id = 0;
    public int missionId;
    private int currentWorkDone;
    [SerializeField] private int numberOfWorkRequired;
    private float currentDuration;
    [SerializeField] private float missionDuration;

    //Public
    public int reward;
    public bool ifMissionAvailable = false;
    public int CurrentWorkDone => currentWorkDone;
    public int NumberOfWorkRequired => numberOfWorkRequired;
    public float CurrentDuration => currentDuration;
    public float DurationLeft
    {
        get { return Mathf.Ceil(missionDuration - currentDuration); }
    }
    public float MissionDuration => missionDuration;

    public Mission(int numberOfWorkUnits, float missionDuration, int missionReward)
    {
        id++;
        missionId = id;
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

    public void SetMissionAvailability(bool _state)
    {
        ifMissionAvailable = _state;
    }
}
