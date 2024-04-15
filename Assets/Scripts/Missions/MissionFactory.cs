using System.Collections;
using System.Collections.Generic;
using Buildings;
using UnityEngine;

public class MissionFactory : MonoBehaviour
{
    
    [SerializeField] private int PalaceBaseIncome = 30;
    [SerializeField] private int TeaHouseBaseIncome = 30;
    [SerializeField] private int UniversityBaseIncome = 30;
    [SerializeField] private int HospitalBaseIncome = 30;
    [Space]
    [SerializeField] private int PalaceBaseTime = 30;
    [SerializeField] private int TeaHouseBaseTime = 30;
    [SerializeField] private int UniversityBaseTime = 30;
    [SerializeField] private int HospitalBaseTime = 30;
    [Space]
    [SerializeField] private float intervalToIncreaseWorkers = 60;

    //Other data
    [HideInInspector] public int additionalWorkersRequired = 0;
    [HideInInspector] private float timeSinceCalled = 0;

    public int GenerateWorkersRequired()
    {
        //Linear pacing - game balancing may go out of hand.
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
