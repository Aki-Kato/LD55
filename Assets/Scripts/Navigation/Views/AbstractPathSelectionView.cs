using Employees.Controllers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Navigation.Views
{
    public abstract class AbstractPathSelectionView : MonoBehaviour
    {
        public event Action PathCompleted;

        [SerializeField] protected Button sendButton;
        [SerializeField] protected Button resetButton;

        protected EmployeeController _employeeController;

        public bool IsSelectionActive { get => enabled; set => enabled = value; }

        protected virtual void OnEnable()
        {
            sendButton.onClick.AddListener(OnSendButtonClick);
            resetButton.onClick.AddListener(OnResetButtonClick);
        }

        protected virtual void OnDisable()
        {
            sendButton.onClick.RemoveListener(OnSendButtonClick);
            resetButton.onClick.RemoveListener(OnResetButtonClick);
        }

        public void SetEmployeeForSelection(EmployeeController employeeController) =>
            _employeeController = employeeController;

        protected abstract void OnSendButtonClick();

        protected abstract void OnResetButtonClick();

        protected void OnPathCompleted() =>
            PathCompleted?.Invoke();
    }
}
