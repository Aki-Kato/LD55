using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMoneyManagerView : MonoBehaviour
{
    [SerializeField] private PlayerMoneyManager playerMoneyManager;

    public TMP_Text currentPlayerMoneyText;

    void OnEnable()
    {
        playerMoneyManager.onMoneyChanged += onMoneyChangedEvent;

    }

    void OnDisable()
    {
        playerMoneyManager.onMoneyChanged -= onMoneyChangedEvent;

    }

    public void onMoneyChangedEvent()
    {
        currentPlayerMoneyText.text = "$" + playerMoneyManager.playerMoney;
    }

}
