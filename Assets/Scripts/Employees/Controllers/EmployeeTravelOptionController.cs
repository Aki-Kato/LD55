using Employees.Enums;
using Employees.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Employees.Controllers
{
    public class EmployeeTravelOptionController : MonoBehaviour
    {
        [SerializeField] private EmployeesWorkController employeesWorkController;
        [SerializeField] private TravelOptions travelOption;
        [SerializeField] private bool isInfinite;
        [SerializeField] private int currentInstances;
        [SerializeField] private int maxInstances;
        [SerializeField] private int cost;
        [Space]
        [SerializeField] private Button buyInstanceButton;
        [SerializeField] private EmployeeTravelOptionButtonView view;

        private void Awake()
        {
            view.Construct(travelOption.ToString(), OnClick);

            if (isInfinite)
                return;

            buyInstanceButton.onClick.AddListener(OnBuyInstanceButtonClick);
            if (maxInstances == 0)
                view.SetLocked(true);

            SetCurrentAmount();
        }

        private void OnDestroy()
        {
            if (isInfinite)
                return;

            buyInstanceButton.onClick.RemoveListener(OnBuyInstanceButtonClick);
        }

        private void OnClick()
        {
            employeesWorkController.SelectTravelOption(travelOption);
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
            SetCurrentAmount();
        }

        private void SetCurrentAmount()
        {
            view.SetAmountText($"{currentInstances}/{maxInstances}");
        }
    }
}

