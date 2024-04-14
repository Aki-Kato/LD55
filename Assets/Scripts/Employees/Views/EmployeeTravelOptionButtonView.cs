using Employees.Controllers;
using Employees.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Employees.Views
{
    [RequireComponent(typeof(Button))]
    public sealed class EmployeeTravelOptionButtonView : MonoBehaviour
    {
        [SerializeField] private EmployeesWorkController workController;
        [SerializeField] private TravelOptions travelOption;
        [SerializeField] private TextMeshProUGUI optionLabel;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);

            optionLabel.text = travelOption.ToString();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            workController.SelectTravelOption(travelOption);
        }

        public void SetActive(bool value)
        {
            _button.interactable = value;
        }
    }
}
