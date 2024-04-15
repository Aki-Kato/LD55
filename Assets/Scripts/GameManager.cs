using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int wonMissions;
    private int lostMissions;
    [SerializeField] private int winGameThreshold = 10;
    [SerializeField] private int loseGameThreshold = 3;
    public UnityEvent onWinGame, onLoseGame;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
            Destroy(gameObject);
    }

    public void BeginGame()
    {

    }

    public void IncrementWinMission()
    {
        wonMissions++;
        if (wonMissions >= winGameThreshold)
        {
            WinGame();
        }
    }

    public void IncrementLoseMission()
    {
        lostMissions++;
        if (lostMissions >= loseGameThreshold)
        {
            LoseGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("Win!");
        Time.timeScale = 0;
        onWinGame?.Invoke();
    }

    private void LoseGame()
    {
        Debug.Log("Lose!");
        Time.timeScale = 0;
        onLoseGame?.Invoke();
    }
}
