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
    public TMP_Text employeeName, employeePerk, employeePerkDescription;

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
            //employeeStats.gameObject.SetActive(true);
            
            // Random name generation and perk for visual purposes, needs to be set up properly and as two different TextMeshPro objects
            employeeName.text = NameGenerator.GenerateName();
            employeePerk.text = employee.listOfPerks.Count > 0 ? employee.listOfPerks[0].perkName : "Missing perk name";
            employeePerkDescription.text = employee.listOfPerks.Count > 0 ? employee.listOfPerks[0].perkDescription : "Missing perk description";
            
            Debug.Log($"Employee: {employee.employeeName}. Speed: {employee.baseRunSpeed}");
        }

        else
        {
            Debug.Log("No Employee Summoned");
        }
    }

    void onSentEmployee(Employee employee)
    {
        //employeeStats.gameObject.SetActive(false);
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
                numberOfAvailableEmployeeText.text = $"{numberOfEmployees} Civil Servants";
            }

            else
            {
                numberOfAvailableEmployeeText.text = "No Civil Servants!";
            }
        }

        if (timerToNewEmployeeText != null)
        {
            float countDownTimer = Mathf.CeilToInt(SummonSystem.instance.IntervalToNewEmployee - SummonSystem.instance.timerToNewEmployee);

            string timerTextValue = ((int)countDownTimer).ToString("D4");
            timerTextValue = timerTextValue.Insert(2, ":");
            timerToNewEmployeeText.text = $"Servant arrives in\n{timerTextValue}";
        }
    }
}
;