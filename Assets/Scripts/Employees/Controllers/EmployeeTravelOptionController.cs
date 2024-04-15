using Employees.Enums;
using Employees.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Employees.Controllers
{
    public class EmployeeTravelOptionController : MonoBehaviour
    {
        [SerializeField] private SummonSystem summonSystem;
        [SerializeField] private EmployeesWorkController employeesWorkController;
        [SerializeField] private TravelOptions travelOption;
        [Space]
        [SerializeField] private bool isInfinite;
        [SerializeField] private int currentInstances;
        [SerializeField] private int maxInstances;
        [SerializeField] private List<int> cost;
        [SerializeField] private int secondsToIncrease;
        [Space]
        [SerializeField] private Button buyInstanceButton;
        [SerializeField] private EmployeeTravelOptionButtonView view;

        private int travelOptionLevel = 0;

        private bool _travelOptionSelectionMode = false;
        private Coroutine _timerCoroutine;

        [Space]
        public UnityEvent onSuccessfulPurchase, onFailedPurchased;

        private void Start()
        {
            view.Construct(travelOption.ToString(), OnClick);
            summonSystem.sentEmployeeEvent += SummonSystem_OnSummonedEmployee;
            employeesWorkController.TravelOptionUsed += EmployeesWorkController_OnTravelOptionUsed;

            if (isInfinite)
                return;

            buyInstanceButton.onClick.AddListener(OnBuyInstanceButtonClick);
            if (maxInstances == 0)
                view.SetLocked(true);

            SetCurrentAmount();
            view.SetUpgradeCostText(cost[travelOptionLevel]);
        }

        private void Update(){
            if (buyInstanceButton != null)
                view.SetUpgradeLocked(PlayerMoneyManager.instance.CheckIfEnoughMoney(GetCurrentCost()) ? true : false);  
        }

        private void OnDestroy()
        {
            summonSystem.sentEmployeeEvent -= SummonSystem_OnSummonedEmployee;
            employeesWorkController.TravelOptionUsed -= EmployeesWorkController_OnTravelOptionUsed;

            if (isInfinite)
                return;

            buyInstanceButton.onClick.RemoveListener(OnBuyInstanceButtonClick);
        }

        private void OnClick()
        {
            employeesWorkController.SelectTravelOption(travelOption);
        }

        private bool IsBlocked()
        {
            return employeesWorkController.BlockHorse && travelOption == TravelOptions.Horse
                || employeesWorkController.BlockCatapult && travelOption == TravelOptions.Catapult;
        }

        private void SummonSystem_OnSummonedEmployee(Employee employee)
        {
            if (IsBlocked())
                return;

            _travelOptionSelectionMode = true;
            if (isInfinite || currentInstances > 0)
                view.SetActive(true);
        }

        private void EmployeesWorkController_OnTravelOptionUsed(TravelOptions travelOption)
        {
            view.SetActive(false);
            _travelOptionSelectionMode = false;
            if (isInfinite || this.travelOption != travelOption)
                return;

            currentInstances--;
            SetCurrentAmount();
        }

        private void OnBuyInstanceButtonClick()
        {
            if (PlayerMoneyManager.instance.TryDecrementMoney(GetCurrentCost()))
            {
                onSuccessfulPurchase.Invoke();

                travelOptionLevel++;
                view.SetUpgradeCostText(GetCurrentCost());

                view.SetLocked(false);
                AddMaxInstances();
                return;
            }

            else
            {
                onFailedPurchased.Invoke();
            }

            Debug.Log("Not enough money!");
        }

        private int GetCurrentCost(){
            if (travelOptionLevel <= cost.Count){
                return cost[travelOptionLevel];
            }

            else return 999999;
        }

        private void AddMaxInstances()
        {
            maxInstances++;
            AddCurrentInstances();
        }

        private void AddCurrentInstances()
        {
            currentInstances++;
            if (_travelOptionSelectionMode && !IsBlocked())
            {
                view.SetActive(true);
            }

            SetCurrentAmount();
        }

        private void SetCurrentAmount()
        {
            view.SetAmountText($"{currentInstances}/{maxInstances}");
            if (currentInstances == 0)
                view.SetActive(false);

            if (currentInstances < maxInstances)
                StartIncreaseCoroutine();
            else
                StopIncreaseCoroutine();
        }

        private void StartIncreaseCoroutine()
        {
            StopIncreaseCoroutine();
            _timerCoroutine = StartCoroutine(IncreaseRoutine());
        }

        private void StopIncreaseCoroutine()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;
                view.SetTimeLeft(0);
            }
        }

        private IEnumerator IncreaseRoutine()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
            int secondsLeft = secondsToIncrease;
            view.SetTimeLeft(secondsLeft);

            while (secondsLeft > 0)
            {
                yield return waitForSeconds;
                secondsLeft--;
                view.SetTimeLeft(secondsLeft);
            }

            AddCurrentInstances();
            _timerCoroutine = null;
        }
    }
}

