using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Employee
{
    public string employeeName;
    public float baseRunSpeed;
    public float baseHorseSpeed;
    public GameObject employeeModel;
    public GameObject horseModel;

    public List<PerkBase> listOfPerks;

}
