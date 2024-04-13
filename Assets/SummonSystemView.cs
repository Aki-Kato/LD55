using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SummonSystemView : MonoBehaviour
{
    public TMP_Text numberOfAvailableEmployeeText;
    public TMP_Text timerToNewEmployeeText;

    //Send Employee
    public Button employeeStatsButton;
    public TMP_Text employeeStats;

    //HireNewEmployee Immediately
    public Button hireNewEmployeeButton;

    [SerializeField] private SummonSystem summonSystem;

    void OnEnable()
    {
        //Needed to add this instead of summonSystem.instance because it will run first before the SummonSystem.instance is initialised, resulting in error
        summonSystem.summonedEmployeeEvent += onSummonedEmployee;
        summonSystem.sentEmployeeEvent += onSentEmployee;
        summonSystem.noMoreEmployeeEvent += onNoMoreEmployee;


    }

    void OnDisable()
    {
        summonSystem.summonedEmployeeEvent -= onSummonedEmployee;
        summonSystem.sentEmployeeEvent -= onSentEmployee;
        summonSystem.noMoreEmployeeEvent -= onNoMoreEmployee;
    }


    void onSummonedEmployee()
    {
        Debug.Log("test");
        Employee employee = SummonSystem.instance.summonedEmployee;
        if (employee != null)
        {
            employeeStatsButton.gameObject.SetActive(true);
            employeeStats.text = $"Name: <Randomised Name>\nSpeed: {employee.speed}";
            Debug.Log($"Employee: {employee.employeeName}. Speed: {employee.speed}");
        }

        else
        {
            Debug.Log("No Employee Summoned");
        }
    }

    void onSentEmployee()
    {
        employeeStatsButton.gameObject.SetActive(false);
    }

    void onNoMoreEmployee()
    {
        hireNewEmployeeButton.gameObject.SetActive(true);
    }

    void Update()
    {
        if (numberOfAvailableEmployeeText != null)
        {
            numberOfAvailableEmployeeText.text = $"{SummonSystem.instance.queueOfAvailableEmployees.Count} Employees Available";
        }

        if (timerToNewEmployeeText != null)
        {
            float countDownTimer = Mathf.CeilToInt(SummonSystem.instance.IntervalToNewEmployee - SummonSystem.instance.timerToNewEmployee);

            timerToNewEmployeeText.text = $"Next Employee in: {countDownTimer}";
        }
    }
}
;