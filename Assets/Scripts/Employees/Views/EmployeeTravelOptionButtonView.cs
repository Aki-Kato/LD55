using System;
using System.Runtime.CompilerServices;
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
        [SerializeField] private TextMeshProUGUI timeLabel;
        [SerializeField] private GameObject lockedSlot;

        [Space]
        [SerializeField] private TextMeshProUGUI upgradeCostLabel;
        [SerializeField] private Button upgradeButton;

        private Button _button;
        private Action _onClickCallback;

        public bool IsLocked => lockedSlot.activeSelf;

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

        public void SetActive(bool value, [CallerMemberName] string kek = "")
        {
            _button.interactable = value;
        }

        public void SetAmountText(string text)
        {
            amountLabel.text = text;
        }

        public void SetUpgradeLocked(bool _state)
        {
            upgradeButton.interactable = _state;
        }
        public void SetUpgradeCostText(int _cost)
        {
            if (upgradeCostLabel != null)
            {
                upgradeCostLabel.text = _cost.ToString();
                if (_cost == 999999)
                {
                    upgradeCostLabel.text = "MAX";
                }
            }
        }

        public void SetTimeLeft(int timeLeft)
        {
            timeLabel.gameObject.SetActive(timeLeft > 0);
            timeLabel.text = $"{FormatTime(timeLeft)}";
        }
    
        private string FormatTime(float seconds)
        {
            int totalSeconds = (int)seconds;
            int minutes = totalSeconds / 60;
            int remainingSeconds = totalSeconds % 60;

            return string.Format("{0:D2}:{1:D2}", minutes, remainingSeconds);
        }
    }
}
