using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSystem : MonoBehaviour
{
    public static SummonSystem instance = null;
    public Employee currentEmployee;
    private bool ifSummonedEmployee = false;
    public float timerToNewEmployee;
    public float IntervalToNewEmployee { get { return intervalToNewEmployee; } }
    [SerializeField] private float intervalToNewEmployee = 30f;

    public List<Employee> debugEmployeeToStart;
    public Employee debugEmployeeToQueue;
    public Queue<Employee> queueOfAvailableEmployees;
    private int oldNumberOfEmployees;

    public event Action<Employee> summonedEmployeeEvent;
    public event Action<Employee> sentEmployeeEvent;
    public event Action changeEmployeeCountEvent;
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

        CheckEmployeeAmount();


        timerToNewEmployee += Time.deltaTime;
    }

    private void UpdateNewEmployeeAvailable()
    {
        //Algorithm to spawn/select new Employees
        queueOfAvailableEmployees.Enqueue(debugEmployeeToQueue);
    }

    private void CheckEmployeeAmount()
    {
        if (queueOfAvailableEmployees.Count != oldNumberOfEmployees)
            changeEmployeeCountEvent?.Invoke();
        
        oldNumberOfEmployees = queueOfAvailableEmployees.Count;
    }


    public void SummonEmployee()
    {
        //If an employee is already summoned and available to be assigned, do not summon new employee
        if (ifSummonedEmployee)
        {
            Debug.Log("Employed already Summoned");
            return;
        }

        ifSummonedEmployee = true;

        currentEmployee = queueOfAvailableEmployees.Dequeue();
        SendEmployee();
        
        summonedEmployeeEvent?.Invoke(currentEmployee);
    }

    public void SendEmployee()
    {
        sentEmployeeEvent?.Invoke(currentEmployee);
    }

    public void ResetEmployee(){
        ifSummonedEmployee = false;
        currentEmployee = null;
    }

    public void HireNewEmployeeImmediately()
    {
        //Check if enough money

        UpdateNewEmployeeAvailable();
    }
}
