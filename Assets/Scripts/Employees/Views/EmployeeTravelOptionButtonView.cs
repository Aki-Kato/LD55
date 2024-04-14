using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Employees.Views
{
    [RequireComponent(typeof(Button))]
    public sealed class EmployeeTravelOptionButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI optionLabel;
        [SerializeField] private TextMeshProUGUI amountLabel;

        [SerializeField] private GameObject lockedSlot;

        private Button _button;
        private Action _onClickCallback;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
        
        public void Construct(string name, Action onClickCallback)
        {
            optionLabel.text = name;
            _onClickCallback = onClickCallback;
        }

        private void OnClick()
        {
            _onClickCallback?.Invoke();
        }

        public void SetLocked(bool value)
        {
            lockedSlot.SetActive(value);
        }

        public void SetActive(bool value)
        {
            _button.interactable = value;
        }

        public void SetAmountText(string text)
        {
            amountLabel.text = text;
        }
    }
}
