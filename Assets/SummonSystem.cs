using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSystem : MonoBehaviour
{
    public Employee summonedEmployee;
    public float timerToNewEmployee;
    [SerializeField] private float intervalToNewEmployee = 30f;

    public List<Employee> debugEmployeeToStart;
    public Queue<Employee> queueOfAvailableEmployees;
    void Start()
    {
        queueOfAvailableEmployees = new Queue<Employee>();

        for (int i = 0; i < debugEmployeeToStart.Count; i++){
            queueOfAvailableEmployees.Enqueue(debugEmployeeToStart[i]);
        }
    }

    void Update()
    {
        if (timerToNewEmployee > Time.time + intervalToNewEmployee)
        {
            timerToNewEmployee = Time.time;
            UpdateNewEmployeeAvailable();
        }
    }

    private void UpdateNewEmployeeAvailable()
    {

    }


    public void SummonEmployee()
    {
        summonedEmployee = queueOfAvailableEmployees.Dequeue();
        Debug.Log($"Employee: {summonedEmployee.employeeName}. Speed: {summonedEmployee.speed}");
    }
}
