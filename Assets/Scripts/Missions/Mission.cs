using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
    public int id;
    private int currentWorkDone;
    [SerializeField] private int numberOfWorkRequired;
    private float currentDuration;
    [SerializeField] private float missionDuration;
    public float reward;

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
        if (currentWorkDone >= numberOfWorkRequired)
        {
            MissionComplete();
        }
    }

    public void MissionComplete()
    {
        //Reward Player
        MissionManager.instance.CompletedMission(this);

        //Destroy Mission
    }

    public void IncrementMissionTimer(float _time){
        currentDuration += _time;
        if (currentDuration >= missionDuration){
            MissionFail();
        }
    }

    public void MissionFail(){
        //Destroy Mission
    }

}
