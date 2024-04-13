using System;
using System.Collections;
using UnityEngine;

public class PlayerMoneyManager : MonoBehaviour
{
    public static PlayerMoneyManager instance = null;
    public int playerMoney = 30;
    public event Action onMoneyChanged;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UpdateMoney();
    }

    void Update()
    {

    }

    public bool TryDecrementMoney(int _cost)
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
