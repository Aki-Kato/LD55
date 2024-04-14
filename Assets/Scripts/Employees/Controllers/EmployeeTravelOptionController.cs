using Employees.Enums;
using Employees.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Employees.Controllers
{
    public class EmployeeTravelOptionController : MonoBehaviour
    {
        [SerializeField] private SummonSystem summonSystem;
        [SerializeField] private EmployeesWorkController employeesWorkController;
        [SerializeField] private TravelOptions travelOption;
        [SerializeField] private bool isInfinite;
        [SerializeField] private int currentInstances;
        [SerializeField] private int maxInstances;
        [SerializeField] private int cost;
        [Space]
        [SerializeField] private Button buyInstanceButton;
        [SerializeField] private EmployeeTravelOptionButtonView view;

        private bool _travelOptionSelectionMode = false;

        private void Start()
        {
            view.Construct(travelOption.ToString(), OnClick);
            summonSystem.summonedEmployeeEvent += SummonSystem_OnSummonedEmployee;
            employeesWorkController.TravelOptionUsed += EmployeesWorkController_OnTravelOptionUsed;

            if (isInfinite)
                return;

            buyInstanceButton.onClick.AddListener(OnBuyInstanceButtonClick);
            if (maxInstances == 0)
                view.SetLocked(true);

            SetCurrentAmount();
        }

        private void OnDestroy()
        {
            summonSystem.summonedEmployeeEvent -= SummonSystem_OnSummonedEmployee;
            employeesWorkController.TravelOptionUsed -= EmployeesWorkController_OnTravelOptionUsed;

            if (isInfinite)
                return;

            buyInstanceButton.onClick.RemoveListener(OnBuyInstanceButtonClick);
        }

        private void OnClick()
        {
            employeesWorkController.SelectTravelOption(travelOption);
        }

        private void SummonSystem_OnSummonedEmployee(Employee employee)
        {
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
            if (PlayerMoneyManager.instance.TryDecrementMoney(cost))
            {
                view.SetLocked(false);
                AddMaxInstances();
                return;
            }

            Debug.Log("Not enough money!");
        }

        private void AddMaxInstances()
        {
            maxInstances++;
            currentInstances++;
            if (_travelOptionSelectionMode)
                view.SetActive(true);

            SetCurrentAmount();
        }

        private void SetCurrentAmount()
        {
            view.SetAmountText($"{currentInstances}/{maxInstances}");
            if (currentInstances == 0)
                view.SetActive(false);
        }
    }
}

