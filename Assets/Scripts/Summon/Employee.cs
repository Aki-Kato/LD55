using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Employee", menuName = "ScriptableObjects/New Employee")]
public class Employee : ScriptableObject
{
    public string employeeName;
    public float speed;
}
