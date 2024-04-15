using System.Collections;
using System.Collections.Generic;
using Buildings;
using UnityEngine;

public static class JobGenerator
{
    static string[] UniversityJobs = { "Research immortality", "Debate scholars", "Copy scrolls", "Observe civil exams"};
    static string[] HospitalJobs = { "Mix remedies", "Pacify ghosts", "Bring good energies", "Study symptoms"};
    static string[] TeahouseJobs = { "Host a ceremony", "Invite a tea blend", "Tea tasting", "Negotiate salaries"};
    static string[] PalaceJobs = { "Tell Emperor's fortune", "Play an opera", "Collect palace gossip", "Arrange a feast"};

    public static string GenerateJob(Building building)
    {
        switch (building.buildingType)
        {
            case BuildingType.University:
                return UniversityJobs[Random.Range(0, UniversityJobs.Length)];
            case BuildingType.Hospital:
                return HospitalJobs[Random.Range(0, HospitalJobs.Length)];
            case BuildingType.Teahouse:
                return TeahouseJobs[Random.Range(0, TeahouseJobs.Length)];
            case BuildingType.Palace:
                return PalaceJobs[Random.Range(0, PalaceJobs.Length)];
            default:
                Debug.Log("No building type recognized");
                return "Pointless labor";
        }
    }
}
