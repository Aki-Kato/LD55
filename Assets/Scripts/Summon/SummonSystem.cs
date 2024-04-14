using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SummonSystem : MonoBehaviour
{
    public static SummonSystem instance = null;
    public Employee currentEmployee;
    private bool ifSummonedEmployee = false;
    public float timerToNewEmployee;
    public float IntervalToNewEmployee { get { return intervalToNewEmployee; } }
    [SerializeField] private float intervalToNewEmployee = 30f;
    [SerializeField] private List<GameObject> modelsForEmployees;

    public List<Employee> debugEmployeeToStart;
    public Employee debugEmployeeToQueue;
    public Queue<Employee> queueOfAvailableEmployees;
    private int oldNumberOfEmployees;

    private List<PerkBase> allPerks;

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

    //////Algorithm to spawn/select new Employees
    private void UpdateNewEmployeeAvailable()
    {
        Employee _newEmployee = new Employee
        {
            employeeName = NameGenerator.GenerateName(),

            //Algorithm for determining speed to be included here.
            speed = 3,

            model = modelsForEmployees[UnityEngine.Random.Range(0, modelsForEmployees.Count)],

            listOfPerks = InitialisePerksForEmployee()
        };
        queueOfAvailableEmployees.Enqueue(_newEmployee);
    }

    private List<PerkBase> InitialisePerksForEmployee(){
        //Random
        int _RNG = UnityEngine.Random.Range(0,3);
        List<PerkBase> _perks = new List<PerkBase>();
        for (int i = 0; i < _RNG; i++){
            _perks.Add(allPerks[UnityEngine.Random.Range(0,allPerks.Count)]);
        }

        return _perks;
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
