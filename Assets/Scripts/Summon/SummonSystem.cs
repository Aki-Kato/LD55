using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SummonSystem : MonoBehaviour
{
    public static SummonSystem instance = null;

    //Public adjustable variables
    [SerializeField] private EmployeeFactory employeeFactory;
    public Employee currentEmployee;
    public float IntervalToNewEmployee { get { return intervalToNewEmployee; } }
    public int numberOfEmployeesToStart = 3;
    [SerializeField] private float intervalToNewEmployee = 30f;

    [Space]
    public List<Employee> debugEmployeeToStart;
    public Employee debugEmployeeToQueue;
    public Queue<Employee> queueOfAvailableEmployees;

    //Private Parameters for internal use
    [HideInInspector] public float timerToNewEmployee;
    private bool ifSummonedEmployee = false;
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
        //for (int i = 0; i < debugEmployeeToStart.Count; i++)
        //{
        //    queueOfAvailableEmployees.Enqueue(debugEmployeeToStart[i]);
        //}
        ///////
        ///

        queueOfAvailableEmployees = new Queue<Employee>();
        for (int i = 0; i < numberOfEmployeesToStart; i++)
        {
            UpdateNewEmployeeAvailable();
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

    //////Algorithm to spawn/select new Employees
    private void UpdateNewEmployeeAvailable()
    {
        queueOfAvailableEmployees.Enqueue(employeeFactory.GenerateNewEmployee());
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

    public void ResetEmployee()
    {
        ifSummonedEmployee = false;
        currentEmployee = null;
    }

    public void HireNewEmployeeImmediately()
    {
        //Check if enough money

        UpdateNewEmployeeAvailable();
    }
}
