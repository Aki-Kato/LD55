using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SummonSystemView : MonoBehaviour
{
    //Summon Employee
    public Button summonEmployeeButton;

    public TMP_Text numberOfAvailableEmployeeText;
    public TMP_Text timerToNewEmployeeText;

    //Send Employee
    public TMP_Text employeeStats;

    //HireNewEmployee Immediately
    public Button hireNewEmployeeButton;

    [SerializeField] private SummonSystem summonSystem;

    void OnEnable()
    {
        //Needed to add this instead of summonSystem.instance because it will run first before the SummonSystem.instance is initialised, resulting in error
        summonSystem.summonedEmployeeEvent += onSummonedEmployee;
        summonSystem.sentEmployeeEvent += onSentEmployee;
        summonSystem.changeEmployeeCountEvent += onChangedEmployeeCount;


    }

    void OnDisable()
    {
        summonSystem.summonedEmployeeEvent -= onSummonedEmployee;
        summonSystem.sentEmployeeEvent -= onSentEmployee;
        summonSystem.changeEmployeeCountEvent -= onChangedEmployeeCount;
    }


    void onSummonedEmployee(Employee employee)
    {
        if (employee != null)
        {
            employeeStats.gameObject.SetActive(true);
            employeeStats.text = $"Name: <Randomised Name>\nSpeed: {employee.speed}";
            Debug.Log($"Employee: {employee.employeeName}. Speed: {employee.speed}");
        }

        else
        {
            Debug.Log("No Employee Summoned");
        }
    }

    void onSentEmployee(Employee employee)
    {
        employeeStats.gameObject.SetActive(false);
    }

    void onChangedEmployeeCount()
    {
        //If there are no more employees, enable the Hire New Employee button, and disable the summonEmployee button. Otherwise, do the opposite.
        int employeeCount = SummonSystem.instance.queueOfAvailableEmployees.Count;
        bool ifNoMoreEmployee = employeeCount == 0 ? true : false;
        hireNewEmployeeButton.gameObject.SetActive(ifNoMoreEmployee);
        summonEmployeeButton.gameObject.SetActive(!ifNoMoreEmployee);
    }

    void Update()
    {
        if (numberOfAvailableEmployeeText != null)
        {
            int numberOfEmployees = SummonSystem.instance.queueOfAvailableEmployees.Count;
            if (numberOfEmployees > 0)
            {
                numberOfAvailableEmployeeText.text = $"{numberOfEmployees} Employees Available";
            }

            else
            {
                numberOfAvailableEmployeeText.text = "No Employees Available!";
            }
        }

        if (timerToNewEmployeeText != null)
        {
            float countDownTimer = Mathf.CeilToInt(SummonSystem.instance.IntervalToNewEmployee - SummonSystem.instance.timerToNewEmployee);

            timerToNewEmployeeText.text = $"Next Employee in: {countDownTimer}";
        }
    }
}
;