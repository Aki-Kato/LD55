using System.Collections;
using System.Collections.Generic;
using Buildings;
using Unity.VisualScripting;
using UnityEngine;

public class MissionGenerator
{
    public const int PalaceBaseIncome = 30;
    public const int TeaHouseBaseIncome = 30;
    public const int UniversityBaseIncome = 30;
    public const int HospitalBaseIncome = 30;
    public const int PalaceBaseTime = 30;
    public const int TeaHouseBaseTime = 30;
    public const int UniversityBaseTime = 30;
    public const int HospitalBaseTime = 30;

    //Linear pacing - game balancing may go out of hand.
    public const float intervalToIncreaseWorkers = 60;
    public int additionalWorkersRequired = 0;
    private float timeSinceCalled = 0;

    public int GenerateWorkersRequired()
    {
        //Pacing for increase in additionalWorkers
        float _interval = Time.time - timeSinceCalled;
        if (_interval > intervalToIncreaseWorkers)
        {
            additionalWorkersRequired++;
            timeSinceCalled = Time.time;
        }

        return Random.Range(1, 3) + additionalWorkersRequired;
    }

    public Mission GenerateMissionAtBuilding(Building building)
    {
        int baseIncome;
        int baseTime;
        int workersRequired = GenerateWorkersRequired();

        switch (building.buildingType)
        {
            case BuildingType.University:
                baseIncome = UniversityBaseIncome;
                baseTime = UniversityBaseTime;
                break;
            case BuildingType.Hospital:
                baseIncome = HospitalBaseIncome;
                baseTime = HospitalBaseTime;
                break;
            case BuildingType.Teahouse:
                baseIncome = TeaHouseBaseIncome;
                baseTime = TeaHouseBaseTime;
                break;
            case BuildingType.Palace:
                baseIncome = PalaceBaseIncome;
                baseTime = PalaceBaseTime;
                break;
            default:
                Debug.Log("No building type recognized");
                baseIncome = 0;
                baseTime = 0;
                break;
        }
        return new Mission(workersRequired, workersRequired*baseTime, workersRequired * baseIncome);
    }
}
