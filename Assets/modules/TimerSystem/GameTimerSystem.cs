using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameTimerSystem : MonoBehaviour
{

    public static GameTimerSystem instance;
    
    // event to be triggered each second
    public delegate void TimerTick(int remainingSeconds);
    public event TimerTick OnTimerTick;
    
    
    public int timeRemaining = 60;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = GetStartingTime();
        OnTimerTick?.Invoke(timeRemaining);
        
        // start coroutine to count down the time every second
        StartCoroutine(CountDown());
    }
    
    public int GetStartingTime()
    {
        return ConfigurationManger.Instance.gameLengthInSeconds;
    }
    IEnumerator CountDown()
    {
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1);
            timeRemaining--;
            OnTimerTick?.Invoke(timeRemaining);
        }
    }

    
    
}
