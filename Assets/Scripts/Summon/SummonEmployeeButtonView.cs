using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SummonEmployeeButtonView : MonoBehaviour
{
    private Button _button;
    private int _previousEmployeeCount;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        SummonSystem.instance.changeEmployeeCountEvent += Instance_OnChangeEmployeeCountEvent;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
        SummonSystem.instance.changeEmployeeCountEvent -= Instance_OnChangeEmployeeCountEvent;
    }

    private void OnClick()
    {
        gameObject.SetActive(false);
    }

    private void Instance_OnChangeEmployeeCountEvent()
    {
        if (_previousEmployeeCount == 0)
            TryEnable();
    }

    public void TryEnable()
    {
        var employeeCount = SummonSystem.instance.queueOfAvailableEmployees.Count;
        bool anyEmployee = employeeCount > 0;
        if (anyEmployee)
            gameObject.SetActive(true);

        _previousEmployeeCount = employeeCount;
    }
}
