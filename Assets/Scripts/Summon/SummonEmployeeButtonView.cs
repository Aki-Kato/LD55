using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SummonEmployeeButtonView : MonoBehaviour
{
    private Button _button;

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
        TryEnable();
    }

    public void TryEnable()
    {
        bool anyEmployee = SummonSystem.instance.queueOfAvailableEmployees.Count > 0;
        if (anyEmployee)
            gameObject.SetActive(true);
    }
}
