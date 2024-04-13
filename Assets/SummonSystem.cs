using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSystem : MonoBehaviour
{
    public static SummonSystem instance = null;
    public Employee summonedEmployee;
    public float timerToNewEmployee;
    public float IntervalToNewEmployee { get { return intervalToNewEmployee; } }
    [SerializeField] private float intervalToNewEmployee = 30f;

    public List<Employee> debugEmployeeToStart;
    public Employee debugEmployeeToQueue;
    public Queue<Employee> queueOfAvailableEmployees;

    public event Action summonedEmployeeEvent;
    public event Action sentEmployeeEvent;
    public event Action noMoreEmployeeEvent;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
            Destroy(gameObject);

        ///////
        ///Debug Purposes - to be removed. To assign initial starting list of employees
        queueOfAvailableEmployees = new Queue<Employee>();
        for (int i = 0; i < debugEmployeeToStart.Count; i++)
        {
            queueOfAvailableEmployees.Enqueue(debugEmployeeToStart[i]);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (timerToNewEmployee >= intervalToNewEmployee)
        {
            timerToNewEmployee = 0;
            UpdateNewEmployeeAvailable();
        }

        CheckEmployeeLeft();

    
        timerToNewEmployee += Time.deltaTime;
    }

    private void UpdateNewEmployeeAvailable()
    {
        //Algorithm to spawn/select new Employees
        queueOfAvailableEmployees.Enqueue(debugEmployeeToQueue);
    }

    private void CheckEmployeeLeft(){
        if (queueOfAvailableEmployees.Count <= 0){
            if (summonedEmployee != null)
            return;

            else {
                noMoreEmployeeEvent?.Invoke();
            }
        }
    }


    public void SummonEmployee()
    {
        //If an employee is already summoned and available to be assigned, do not summon new employee
        if (summonedEmployee != null)
        {
            Debug.Log("Employed already Summoned");
            return;
        }

        summonedEmployee = queueOfAvailableEmployees.Dequeue();
        summonedEmployeeEvent?.Invoke();
    }

    public void SendEmployee(){
        //Insert sending employee to mission code here..

        //Debug/prototype
        summonedEmployee = null;
        Debug.Log("Sent Employee");

        sentEmployeeEvent?.Invoke();
    }

    public void HireNewEmployeeImmediately(){
        //Check if enough money

        UpdateNewEmployeeAvailable();
    }
}
