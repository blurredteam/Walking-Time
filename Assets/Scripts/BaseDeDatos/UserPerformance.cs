using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPerformance : MonoBehaviour
{
    public static UserPerformance instance;

    [SerializeField] public float timer = 0f;
    private bool timerPaused = false;

    [SerializeField] public int totalGoldGained = 0;
    private int previousGold = 0;
    private int currentGold = 0;

    [SerializeField] public int puzzlesPlayed;
    [SerializeField] public int puzzlesWon;
    [SerializeField] public int puzzlesLost;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (timerPaused == false)
        {
            timer += Time.deltaTime;
        }
    }

    //////////////////////////////TIMER
    public void changeTimerState()
    {
        if (timerPaused == false)
        {
            timerPaused = true;
        }
        else { timerPaused = false; }
    }

    public float GetTimerValue()
    {
        return timer;
    }

    /////////////////////////////GOLD

    public void updateGold()
    {
        previousGold = currentGold;
        currentGold = LevelManager.instance.gold;
        if (currentGold > previousGold)
        {
            totalGoldGained += (currentGold - previousGold);
        }
    }

    /////////////////////////////PUZZLES WON

    public void updatePuzzlesPlayed(int value)
    {
        puzzlesPlayed += 1;
        if (value == 1)
        {
            puzzlesWon += 1;
        }
        else
        {
            puzzlesLost += 1;
        }
    }


    /////////////////////////////////////RESET
    public void resetStats()
    {
        timer = 0f;
        timerPaused = false;
        totalGoldGained = 0;
        previousGold = 0;
        currentGold = 0;
        puzzlesPlayed = 0;
        puzzlesWon = 0;
        puzzlesLost = 0;
    }
}
