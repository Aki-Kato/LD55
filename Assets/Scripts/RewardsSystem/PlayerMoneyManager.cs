using System;
using System.Collections;
using UnityEngine;

public class PlayerMoneyManager : MonoBehaviour
{
    public int playerMoney = 30;
    public event Action onMoneyChanged;
    void Start()
    {

    }

    void Update()
    {

    }

    public bool TryPurchaseSomething(int _cost)
    {
        if (playerMoney >= _cost)
        {
            DecrementMoney(_cost);
            return true;
        }

        else return false;
    }

    private void DecrementMoney(int _delta)
    {
        playerMoney -= _delta;
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        onMoneyChanged?.Invoke();
    }
}
